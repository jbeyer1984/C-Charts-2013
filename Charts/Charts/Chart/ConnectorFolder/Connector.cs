using Charts.Chart.ConnectorFolder.ConnectorInterfaces;
using Charts.Chart.Debug;
using Charts.Chart.Identifier;
using Charts.Factories;
using System;
using System.Collections.Generic;

namespace Charts.Chart.ConnectorFolder
{
    /// <summary>
    /// This class is used for binding dependencies of classes with logging implemented
    /// 
    /// scenario: bind ChartForm obj to ChartPanel obj, that it can be used
    ///           later for chartPanel.chartForm call
    /// a dictionary will be used with a string identifier like ChartForm_ChartPanel
    /// 
    /// usages *chaining is used*:
    /// 1. connect(targetClass).by(externalClass) look below call 1.2 will be used like targetClass.externalClass
    /// 1.2 with(targetClass).getByType(externalClass.GetType())
    /// 1.3 with(targetClass).forceCreation().getByType(anyClassType) is later used for cache implementation in other class
    /// 1.4 with(targetClass).remove().getByType(anyClassType) is used for removing entries
    /// 
    /// dependencies: classes must implement IIdentifier
    /// advantages: the classes must implement IIdentifier interface and logging is available
    /// disadvantages: there are not needed any getters and setters, so sometime it is not obvious
    /// </summary>
    public class Connector : IConnector
    {
        private Dictionary<String, Dictionary<object, object>> dictionary = new Dictionary<String, Dictionary<object, object>>();

        private object withTemp = null;
        private object byTemp = null;
        private String identifier = null;
        private String directionBack = "<-";
        private String directionForward = "->";
        private bool isNullAble = false;
        private bool isCreateAble = false;
        private bool isShouldBeRemoved = false;
        private bool hasBeenCreatedNew = false;

        public IConnector connect(object obj)
        {
            checkIIdentifier(obj);
            setBothNull();

            withTemp = obj;

            return this;
        }

        protected void checkIIdentifier(object obj)
        {
            if (!(obj is IIdentifier)) { // @todo maybe extract to Checker Class
                throw new Exception(string.Format(
                    "object to insert in dictionary has not interface IIdentifier, {0}",
                    obj.GetType().Name
                ));
            }
        }

        public void by(object obj)
        {
            checkIIdentifier(obj);
            checkFromNotNull();

            byTemp = obj;
            identifier = withTemp.GetType().Name + "_" + byTemp.GetType().Name;
            if (dictionary.ContainsKey(identifier)) {
                if (!dictionary[identifier].ContainsKey(withTemp)) {
                    hasBeenCreatedNew = true;
                    dictionary[identifier].Add(withTemp, byTemp);
                }
            } else {
                Dictionary<object, object> dictionaryTemp = new Dictionary<object, object>();
                dictionaryTemp.Add(withTemp, byTemp);
                dictionary.Add(identifier, dictionaryTemp);
                hasBeenCreatedNew = true;
            }

            if (hasBeenCreatedNew) {
                logConnect(
                    ((IIdentifier)withTemp).Identifier,
                    ((IIdentifier)byTemp).Identifier,
                    directionBack
                );
                hasBeenCreatedNew = false;
            }
        }

        protected void checkFromNotNull()
        {
            if (null == withTemp) {
                throw new Exception(string.Format("WithTemp is NULL!, {0}", withTemp));
            }
        }

        protected void setBothNull()
        {
            withTemp = null;
            byTemp = null;
        }

        public IConnector with(object obj)
        {
            setBothNull();

            withTemp = obj; // yes its in the other direction to toTemp is true ;)

            return this;
        }

        public IConnector canBeNull()
        {
            isNullAble = true;

            return this;
        }

        public IConnector forceCreation()
        {
            isCreateAble = true;

            return this;
        }

        public IConnector remove() // only used before getByType !!!
        {
            isShouldBeRemoved = true;

            return this;
        }

        public object getByType(Type className)
        {
            checkFromNotNull();

            identifier = withTemp.GetType().Name + "_" + className.Name;

            if (dictionary.ContainsKey(identifier)) {
                //if (isCreateAble) {
                //    this.createNewWithEntryByClassName(withTemp, className);
                //}

                if (dictionary[identifier].ContainsKey(withTemp)) {
                    byTemp = dictionary[identifier][withTemp];
                }
            }

            if (isShouldBeRemoved) {
                if (dictionary.ContainsKey(identifier)) {
                    if (dictionary[identifier].ContainsKey(withTemp)) {
                        dictionary[identifier].Remove(withTemp);
                        isShouldBeRemoved = false;
                    }
                }
            } else if (isNullAble && null == byTemp) {
                isNullAble = false;
                return null;
            } else if (isCreateAble && null == byTemp) {
                createNewWithEntryByClassName(withTemp, className);
                isCreateAble = false;
            } else if (null == byTemp) {
                throw new Exception(string.Format("both are not connected WithTemp {0}, ByTemp {1} mit ClassName {2}", withTemp, byTemp, className));
            }

            if (isCreateAble) {
                isCreateAble = false;
            }

            if (className != byTemp.GetType()) {
                throw new Exception("wrong className in dictionary should be " + className.Name);
            }

            logGet(
                ((IIdentifier)withTemp).Identifier,
                ((IIdentifier)byTemp).Identifier,
                directionForward
            );

            return byTemp;
        }

        protected void createNewWithEntryByClassName(object with, Type className)
        {
            object by = Activator.CreateInstance(className);
            Inst.getStaticCall().initIdentifierOneTime(by);
            connect(with).by(by);
            byTemp = by;
        }

        protected void logGet(String classNameFrom, String classNameTo, String direction = "->")
        {
            logConnect(classNameTo, classNameFrom, direction);
        }

        protected void logConnect(String classNameFrom, String classNameTo, String direction = "<-")
        {
            if (!DebugSettings.debug) {
                return;
            }

            if ("<-cache-".Equals(direction)) {
                DebugSettings.log(string.Format("{0} {1} {2}", classNameFrom, direction, classNameTo));
            } else {
                DebugSettings.log(string.Format("{0} {1} {2}", classNameTo, direction, classNameFrom));
            }
        }

        public object WithTemp
        {
            get { return withTemp; }
            set { withTemp = value; }
        }

        public object ByTemp
        {
            get { return byTemp; }
            set { byTemp = value; }
        }

        public String DirectionForward
        {
            get { return directionForward; }
            set { directionForward = value; }
        }

        public String DirectionBack
        {
            get { return directionBack; }
            set { directionBack = value; }
        }
    }
}
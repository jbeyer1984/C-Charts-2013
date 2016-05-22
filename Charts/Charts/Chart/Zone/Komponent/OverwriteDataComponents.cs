namespace Charts
{
    public class OverwriteDataComponents
    {
        private CollectionDrawerOverwrite collectionDrawerOverwrite;

        /// <summary>
        /// is used to inject registered behaviour later
        /// 
        /// @todo built a class for binding properties to chart panel and OverwriteDataComponents
        /// </summary>
        public CollectionDrawerOverwrite CollectionDrawerOverwrite
        {
            get { return collectionDrawerOverwrite; }
            set { collectionDrawerOverwrite = value; }
        }
    }
}
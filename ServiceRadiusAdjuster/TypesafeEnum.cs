namespace ServiceRadiusAdjuster
{
    //typesafe enum pattern
    public abstract class TypesafeEnum
    {
        private readonly string name;

        protected TypesafeEnum(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }
}

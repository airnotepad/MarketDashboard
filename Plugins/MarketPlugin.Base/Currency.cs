namespace MarketPlugin.Base
{
    public partial class Currency
    {
        public Currency(string Name, string ISO)
        {
            this.Name = Name;
            this.ISO = ISO;
        }

        public string Name { get; }
        public string ISO { get; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var eqObj = obj as Currency;

            return Name == eqObj.Name
                && ISO == eqObj.ISO;
        }

        public static bool operator !=(Currency lhs, Currency rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return false;
                }

                return true;
            }

            return !lhs.Equals(rhs);
        }

        public static bool operator ==(Currency lhs, Currency rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }
    }
}

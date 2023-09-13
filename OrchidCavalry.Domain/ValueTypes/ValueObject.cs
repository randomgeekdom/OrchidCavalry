namespace OrchidCavalry.Models.ValueTypes
{
    public abstract class ValueObject<T> : IComparable
        where T : IComparable
    {
        public ValueObject(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj is not ValueObject<T> valueObj)
                return 1;

            return this.Value.CompareTo(valueObj.Value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject<T> valueObj)
                return false;

            return Value.Equals(valueObj.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
namespace OrchidCavalry.Models.ValueTypes
{
    public abstract class ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj is not ValueObject<T> valueObj)
                return false;

            return Value.Equals(valueObj.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


        public T Value { get; set; }
    }
}
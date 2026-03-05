namespace CB.BackDefault.Domain.ValueObjects.Base
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject other)
                return false;

            using var thisValues = GetEqualityComponents().GetEnumerator();
            using var otherValues = other.GetEqualityComponents().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                    return false;

                if (thisValues.Current is not null &&
                    !thisValues.Current.Equals(otherValues.Current))
                    return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var obj in GetEqualityComponents())
                hash.Add(obj);

            return hash.ToHashCode();
        }
    }
}

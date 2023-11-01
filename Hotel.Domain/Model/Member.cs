using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Member
    {
        public Member(string name, DateOnly birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }
        private string _name;
        private DateOnly _birthDay;
        public string Name { get { return _name; } set { if (string.IsNullOrWhiteSpace(value)) throw new MemberException("name is empty"); _name = value; } }
        public DateOnly BirthDay { get { return _birthDay; } set { if (value > DateOnly.FromDateTime(DateTime.Now)) throw new MemberException("birthday invalid"); _birthDay = value; } }

        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   _name == member._name &&
                   _birthDay.Equals(member._birthDay);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _birthDay);
        }
    }
}
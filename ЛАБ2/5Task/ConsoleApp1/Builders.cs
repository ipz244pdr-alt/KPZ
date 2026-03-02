using System.Collections.Generic;

namespace ConsoleApp1
{
    public class HeroBuilder : ICharacterBuilder
    {
        private Character _character = new Character();

        public HeroBuilder SetName(string name) { _character.Name = name; return this; }
        public HeroBuilder SetAppearance(string h, string b, string hair, string e)
        {
            _character.Height = h; _character.Build = b;
            _character.HairColor = hair; _character.EyeColor = e;
            return this;
        }
        public HeroBuilder AddEquipment(string item) { _character.Inventory.Add(item); return this; }

        public HeroBuilder AddGoodDeed(string deed) { _character.Deeds.Add(deed); return this; }

        ICharacterBuilder ICharacterBuilder.SetName(string n) => SetName(n);
        ICharacterBuilder ICharacterBuilder.SetAppearance(string h, string b, string ha, string e) => SetAppearance(h, b, ha, e);
        ICharacterBuilder ICharacterBuilder.AddEquipment(string i) => AddEquipment(i);

        public Character Build() => _character;
    }

    public class EnemyBuilder : ICharacterBuilder
    {
        private Character _character = new Character();

        public EnemyBuilder SetName(string name) { _character.Name = name; return this; }
        public EnemyBuilder SetAppearance(string h, string b, string hair, string e)
        {
            _character.Height = h; _character.Build = b;
            _character.HairColor = hair; _character.EyeColor = e;
            return this;
        }
        public EnemyBuilder AddEquipment(string item) { _character.Inventory.Add(item); return this; }

        public EnemyBuilder AddEvilDeed(string deed) { _character.Deeds.Add(deed); return this; }

        ICharacterBuilder ICharacterBuilder.SetName(string n) => SetName(n);
        ICharacterBuilder ICharacterBuilder.SetAppearance(string h, string b, string ha, string e) => SetAppearance(h, b, ha, e);
        ICharacterBuilder ICharacterBuilder.AddEquipment(string i) => AddEquipment(i);

        public Character Build() => _character;
    }
}
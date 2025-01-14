using System;

namespace Server.Items
{
    public class ArcaneFocus : TransientItem
    {
        [Constructible]
        public ArcaneFocus()
            : this(TimeSpan.FromHours(1), 1)
        {
        }

        [Constructible]
        public ArcaneFocus(int lifeSpan, int strengthBonus)
            : this(TimeSpan.FromSeconds(lifeSpan), strengthBonus)
        {
        }

        public ArcaneFocus(TimeSpan lifeSpan, int strengthBonus) : base(0x3155, lifeSpan)
        {
            LootType = LootType.Blessed;
            StrengthBonus = strengthBonus;
        }

        public ArcaneFocus(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber => 1032629; // Arcane Focus

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrengthBonus { get; set; }

        public override TextDefinition InvalidTransferMessage => 1073480; // Your arcane focus disappears.
        public override bool Nontransferable => true;

        public override void GetProperties(IPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060485, $"{StrengthBonus}"); // strength bonus ~1_val~
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            writer.Write(StrengthBonus);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();

            StrengthBonus = reader.ReadInt();
        }
    }
}

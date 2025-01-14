using Server.Gumps;
using Server.Network;

namespace Server.Engines.Quests.Collector
{
    public class PaintedImage : Item
    {
        private ImageType m_Image;

        [Constructible]
        public PaintedImage(ImageType image) : base(0xFF3)
        {
            Weight = 1.0;
            Hue = 0x8FD;

            m_Image = image;
        }

        public PaintedImage(Serial serial) : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ImageType Image
        {
            get => m_Image;
            set
            {
                m_Image = value;
                InvalidateProperties();
            }
        }

        public override void AddNameProperty(IPropertyList list)
        {
            var info = ImageTypeInfo.Get(m_Image);
            list.Add(1060847, $"#1055126\t#{info.Name}"); // a painted image of:
        }

        public override void OnSingleClick(Mobile from)
        {
            var info = ImageTypeInfo.Get(m_Image);
            LabelTo(from, 1060847, $"#1055126\t#{info.Name}"); // a painted image of:
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            from.SendGump(new InternalGump(m_Image));
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.WriteEncodedInt((int)m_Image);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            m_Image = (ImageType)reader.ReadEncodedInt();
        }

        private class InternalGump : Gump
        {
            public InternalGump(ImageType image) : base(75, 25)
            {
                var info = ImageTypeInfo.Get(image);

                AddBackground(45, 20, 100, 100, 0xA3C);
                AddBackground(52, 29, 86, 82, 0xBB8);

                AddItem(info.X, info.Y, info.Figurine);
            }
        }
    }
}

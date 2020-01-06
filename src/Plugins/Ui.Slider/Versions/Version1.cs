using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.MediaEntities;
using Ui.Slider.Data;
using Db = DotEntity.DotEntity.Database;
namespace Ui.Slider.Versions
{
    public class Version1 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<UiSlider>(transaction);
            Db.CreateConstraint(Relation.Create<Media, UiSlider>("Id", "MediaId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropConstraint(Relation.Create<Media, UiSlider>("Id", "MediaId"), transaction);
            Db.DropTable<UiSlider>(transaction);
        }

        public string VersionKey { get; } = "Ui.Slider.Version.1";
    }
}
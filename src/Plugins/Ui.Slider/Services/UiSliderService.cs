using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Extensions;
using Ui.Slider.Data;

namespace Ui.Slider.Services
{
    public class UiSliderService : FoundationEntityService<UiSlider>, IUiSliderService
    {
        public override UiSlider Get(int id)
        {
            return Repository.Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<UiSlider, Media>())
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }

        public override IEnumerable<UiSlider> Get(Expression<Func<UiSlider, bool>> where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<Media>("MediaId", "Id")
                .Relate(RelationTypes.OneToOne<UiSlider, Media>())
                .OrderBy(x => x.DisplayOrder)
                .Where(where)
                .SelectNested(page, count);
        }
    }
}
using Cards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Core.Specifications
{
    public class FilterCardSpecification : BaseSpecification<Card>
    {
        public FilterCardSpecification(CardSpecPrams cardSpecPrams) :
            base      
            (x =>
            (string.IsNullOrEmpty(cardSpecPrams.Search) || x.Name == cardSpecPrams.Search) &&
            (!String.IsNullOrEmpty(cardSpecPrams.Color) || x.Color == cardSpecPrams.Color)
             && (cardSpecPrams.CreatedOn.HasValue) || x.CreatedOn == cardSpecPrams.CreatedOn)
        {

        }
    }
}

﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Areas.Administration.Models.Cultures;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Localization;

namespace EvenCart.Areas.Administration.Factories.Cultures
{
    public class CurrencyModelFactory : ICurrencyModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IGenesisEngine GenesisEngine;
        public CurrencyModelFactory(IModelMapper modelMapper, IGenesisEngine genesisEngine)
        {
            _modelMapper = modelMapper;
            GenesisEngine = genesisEngine;
        }

        public CurrencyModel Create(Currency entity)
        {
            var model = _modelMapper.Map<CurrencyModel>(entity);
            model.FlagUrl = GenesisEngine.MapUrl($"~/common/flags/{entity.Flag}");
            return model;
        }
    }
}
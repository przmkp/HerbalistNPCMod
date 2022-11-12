using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

namespace HerbalistNPC
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Shop price multiplier")]
        [DefaultValue(2f)]
        [Range(0f, 20f)]
        [Increment(0.1f)]
        public float ShopMultiplier;


        public override void OnChanged()
        {
            
        }
    }
}
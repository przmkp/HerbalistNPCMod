using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;

namespace HerbalistNPC
{
	public class HerbalistNPC : Mod
	{
	}

	[AutoloadHead]
	public class Herbalist : ModNPC
	{
		public override string Texture => "HerbalistNPC/HerbalistNPC";
		public override bool Autoload(ref string name)
		{
			name = "Herbalist";
			return mod.Properties.Autoload;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herbalist");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			for (int k = 0; k < 255; k++)
			{
				Player player = Main.player[k];
				if (!player.active)
				{
					continue;
				}

				if (NPC.downedSlimeKing) { return true; }
			}
			return false;
		}

		public override string TownNPCName()
		{
			string[] names = { "Zdisek", "Miroslav", "Krzesimir", "Belos", "Milo", "Luborek", "Stanek", "Vacek", "Dragutin", "Stanimir", "Vojmil" };
			return names[WorldGen.genRand.Next(names.Length)];
		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int pirate = NPC.FindFirstNPC(NPCID.Pirate);
			//int zoologist = NPC.FindFirstNPC(NPCID.Zoologist);
			int dryad = NPC.FindFirstNPC(NPCID.Dryad);
			int demolitionist = NPC.FindFirstNPC(NPCID.Demolitionist);
			int wizard = NPC.FindFirstNPC(NPCID.Wizard);
			int angler = NPC.FindFirstNPC(NPCID.Angler);

			if (pirate >= 0)
			{
				chat.Add($"Do not give any Blinkroot to {Main.npc[pirate].GivenName}, he uses it to make this awful drink...");
			}
            //if (zoologist >= 0)
            //{
            //    chat.Add($"I found a correlation between blooming of the Deathweed and {Main.npc[zoologist].GivenName}'s transformations.");
            //}
			if (dryad >= 0)
            {
				chat.Add($"Maybe a Deathweed wasn't the best gift for {Main.npc[dryad].GivenName}...");
			}
			if (demolitionist >= 0)
			{
				chat.Add($"{Main.npc[demolitionist].GivenName} told me he had used my precious Fireblossom to make a bomb! What a waste!");
			}
			if (wizard >= 0)
			{
				chat.Add($"So I showed {Main.npc[wizard].GivenName} a Moonglow and he said that it is full of magic potential. Whatever that means.");
			}
			if (angler >= 0)
			{
				chat.Add($"It seems to me that {Main.npc[angler].GivenName} is allergic to Waterleafs...");
			}
			chat.Add("Yesterday I found this cave full of Blinkroot... Nevermind, it's empty now.", 0.3);
			chat.Add("Did you know that Blinkroot grows fastest in total darkness?");
			chat.Add("Dayblooms are so cute, even monsters want to collect them!");
			chat.Add("Did you know that Dayblooms grow during the daytime? Fascinating!");
			chat.Add("Deathweed blooms during a Blood Moon or a Full Moon at night.");
			chat.Add("Don't even try to eat Deathweed. The name tells why.", 0.1);
			chat.Add("Fireblossoms will bloom when the sun is setting, unless it is raining.");
			chat.Add("Did you know that Fireblossoms are immune to lava? Fascinating!");
			chat.Add("How to find a blooming Fireblossom? It will shoot a stream of sparks into the air.");
			chat.Add("Did you know that Moonglow only blooms at night? Fascinating!");
			chat.Add("No, that's not a Snowdrop, it's a Moonglow.");
			chat.Add("Did you know that Shiverthorn grows naturally on snow and ice? Fascinating!");
			chat.Add("Last night I had a dream about leaving this place and starting a Shiverthorn farm somewhere cold.", 0.2);
			chat.Add("The Shiverthorn will usually bloom after... some time.");
			chat.Add("Did you know that Waterleaf grows on sand? Not really fascinating.");
			chat.Add("Waterleafs somehow know when the water is falling from the sky.", 0.4);
			chat.Add("Waterleaf will never spawn naturally in the Ocean biome.");
			chat.Add("Be patient. The Waterleaf takes the longest to bloom out of all the herbs.");
			return chat.Get();
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ItemID.Acorn);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Daybloom);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.DaybloomSeeds);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Blinkroot);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.BlinkrootSeeds);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Deathweed);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.DeathweedSeeds);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Shiverthorn);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.ShiverthornSeeds);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Moonglow);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.MoonglowSeeds);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Waterleaf);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.WaterleafSeeds);
			nextSlot++;
			if (NPC.downedBoss2)
			{
				shop.item[nextSlot].SetDefaults(ItemID.Fireblossom);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.FireblossomSeeds);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ItemID.ViciousMushroom);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.VileMushroom);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
			nextSlot++;
			if (NPC.downedBoss1)
            {
				shop.item[nextSlot].SetDefaults(ItemID.CorruptSeeds);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.CrimsonSeeds);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.MushroomGrassSeeds);
				nextSlot++;
			}
			if (Main.hardMode)
            {
				shop.item[nextSlot].SetDefaults(ItemID.HallowedSeeds);
				nextSlot++;
			}
		}

		public override bool CanGoToStatue(bool toKingStatue)
		{
			return true;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 10;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.WaterBolt;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;


namespace HerbalistNPC
{
	public class HerbalistNPC : Mod
	{
	}

	[AutoloadHead]
	public class Herbalist : ModNPC
	{
		public float GetShopPriceMultiplier() {
			return ModContent.GetInstance<Config>().ShopMultiplier;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 90;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 4;

			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f,
				Direction = 1
			};

			NPC.Happiness
				.SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
				.SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
				.SetBiomeAffection<JungleBiome>(AffectionLevel.Love)
				.SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
				.SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like)
				.SetNPCAffection(NPCID.Pirate, AffectionLevel.Dislike)
			;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.HerbalistNPC.Bestiary.Herbalist")),
			});
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

		public override ITownNPCProfile TownNPCProfile()
		{
			return new HerbalistProfile();
		}

		public override List<string> SetNPCNameList()
		{
			return new List<string>() { 
				"Zdisek",
				"Miroslav",
				"Krzesimir",
				"Belos",
				"Milo",
				"Luborek",
				"Stanek",
				"Vacek",
				"Dragutin",
				"Stanimir",
				"Vojmil"
			};
		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int pirate = NPC.FindFirstNPC(NPCID.Pirate);
			int zoologist = NPC.FindFirstNPC(NPCID.BestiaryGirl);
			int dryad = NPC.FindFirstNPC(NPCID.Dryad);
			int demolitionist = NPC.FindFirstNPC(NPCID.Demolitionist);
			int wizard = NPC.FindFirstNPC(NPCID.Wizard);
			int angler = NPC.FindFirstNPC(NPCID.Angler);

			if (pirate >= 0)
			{
				chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.PirateDialogue", Main.npc[pirate].GivenName));
			}
            if (zoologist >= 0)
            {
                chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.ZoologistDialogue", Main.npc[zoologist].GivenName));
            }
            if (dryad >= 0)
            {
				chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DryadDialogue", Main.npc[dryad].GivenName));
			}
			if (demolitionist >= 0)
			{
				chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DemolitionistDialogue", Main.npc[demolitionist].GivenName));
			}
			if (wizard >= 0)
			{
				chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.WizardDialogue", Main.npc[wizard].GivenName));
			}
			if (angler >= 0)
			{
				chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.AnglerDialogue", Main.npc[angler].GivenName));
			}
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.BlinkrootDialogue1"), 0.3);
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.BlinkrootDialogue2"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DaybloomDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DaybloomDialogue2"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DeathweedDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.DeathweedDialogue2"), 0.1);
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.FireblossomDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.FireblossomDialogue2"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.FireblossomDialogue3"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.MoonglowDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.MoonglowDialogue2"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.ShiverthornDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.ShiverthornDialogue2"), 0.2);
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.ShiverthornDialogue3"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.WaterleafDialogue1"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.WaterleafDialogue2"), 0.4);
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.WaterleafDialogue3"));
			chat.Add(Language.GetTextValue("Mods.HerbalistNPC.Dialogue.Herbalist.WaterleafDialogue4"));
			return chat;
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
			var items = new List<int> {
				ItemID.Acorn,
				ItemID.Daybloom,
				ItemID.DaybloomSeeds,
				ItemID.Blinkroot,
				ItemID.BlinkrootSeeds,
				ItemID.Deathweed,
				ItemID.DeathweedSeeds,
				ItemID.Shiverthorn,
				ItemID.ShiverthornSeeds,
				ItemID.Moonglow,
				ItemID.MoonglowSeeds,
				ItemID.Waterleaf,
				ItemID.WaterleafSeeds,
			};
			
			if (NPC.downedBoss2)
			{
				items.Add(ItemID.Fireblossom);
				items.Add(ItemID.FireblossomSeeds);
			}
			items.Add(ItemID.ViciousMushroom);
			items.Add(ItemID.VileMushroom);
			items.Add(ItemID.Mushroom);
			items.Add(ItemID.GlowingMushroom);
			if (NPC.downedBoss1)
            {
				items.Add(ItemID.CorruptSeeds);
				items.Add(ItemID.CrimsonSeeds);
				items.Add(ItemID.MushroomGrassSeeds);
			}
			if (Main.hardMode)
            {
				items.Add(ItemID.HallowedSeeds);
			}

			foreach (var item in items)
			{
				shop.item[nextSlot].SetDefaults(item);
				shop.item[nextSlot].shopCustomPrice = (int)(shop.item[nextSlot].value * GetShopPriceMultiplier());
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

	public class HerbalistProfile : ITownNPCProfile
	{
		public int RollVariation() => 0;
		public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

		public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
		{
			if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
				return ModContent.Request<Texture2D>("HerbalistNPC/Herbalist");

			if (npc.altTexture == 1)
				return ModContent.Request<Texture2D>("HerbalistNPC/Herbalist");

			return ModContent.Request<Texture2D>("HerbalistNPC/Herbalist");
		}

		public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("HerbalistNPC/Herbalist_Head");
	}
}
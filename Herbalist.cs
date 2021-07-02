using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;

namespace Herbalist
{
	public class Herbalist : Mod
	{
	}

	[AutoloadHead]
	public class HerbalistNPC : ModNPC
	{
		public override string Texture => "Herbalist/Herbalist";
		public override bool Autoload(ref string name)
		{
			name = "Herbalist";
			return mod.Properties.Autoload;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Person");
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
			switch (WorldGen.genRand.Next(4))
			{
				case 0:
					return "Nobody";
				case 1:
					return "Somebody";
				case 2:
					return "Noone";
				default:
					return "Someone";
			}
		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4))
			{
				chat.Add("Can you please tell " + Main.npc[partyGirl].GivenName + " to stop decorating my house with colors?");
			}
			chat.Add("Sometimes I feel like I'm different from everyone else here.");
			chat.Add("What's your favorite color? My favorite colors are white and black.");
			chat.Add("What? I don't have any arms or legs? Oh, don't be ridiculous!");
			chat.Add("This message has a weight of 5, meaning it appears 5 times more often.", 5.0);
			chat.Add("This message has a weight of 0.1, meaning it appears 10 times as rare.", 0.1);
			return chat.Get();
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Shop";
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
			damage = 30;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
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
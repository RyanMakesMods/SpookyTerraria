﻿using Microsoft.Xna.Framework;
using SpookyTerraria.Utilities;
using SpookyTerraria.OtherItems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Flashlight
{
    public static class CustomItemHoldStyleID
    {
        public static int LookAtCursor = 16;
    }
    public class Flashlight : ModItem
    {
        public bool isActive = true;
        // TODO: Fix HoldStyle
        public float itemRotation = 0;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use this to light up where you point around you in the direction of your mouse\nCan be used as a makeshift weapon");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.holdStyle = CustomItemHoldStyleID.LookAtCursor;
            item.rare = ItemRarityID.LightRed;
            item.damage = 6;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.useAnimation = 10;
            item.useTime = 10;
            item.knockBack = 2.5f;
            item.useTurn = true;
        }
        public override void UpdateInventory(Player player)
        {
            // ⬛⬛⬛⬛⬛⬛⬛⬛☐
            if (player.CountItem(ModContent.ItemType<Battery>()) == 0)
            {
                item.SetNameOverride($"Flashlight (☐☐☐☐☐)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) == 1)
            {
                item.SetNameOverride($"Flashlight (⬛☐☐☐☐)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) == 2)
            {
                item.SetNameOverride($"Flashlight (⬛⬛☐☐☐)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) == 3)
            {
                item.SetNameOverride($"Flashlight (⬛⬛⬛☐☐)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) == 4)
            {
                item.SetNameOverride($"Flashlight (⬛⬛⬛⬛☐)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) == 5)
            {
                item.SetNameOverride($"Flashlight (⬛⬛⬛⬛⬛)");
            }
            if (player.CountItem(ModContent.ItemType<Battery>()) > 5)
            {
                item.SetNameOverride($"Flashlight (⬛⬛⬛⬛⬛+)");
            }
        }
        public override void PostUpdate()
        {
            item.SetNameOverride($"Flashlight");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe.AddIngredient(ItemID.Glass, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool HoldItemFrame(Player player)
        {
            if (Main.myPlayer != player.whoAmI)
            {
                return true;
            }
            Vector2 playerToCursor = (Main.MouseWorld - player.Center);
            playerToCursor.Normalize();
            player.bodyFrame.Y = Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) * player.bodyFrame.Height;
            if (Main.MouseWorld.X > player.Center.X)
            {
                player.ChangeDir(1);
            }
            else
            {
                player.ChangeDir(-1);
            }
            return true;
        }
        public override void HoldStyle(Player player)
        {
            if (!Main.gameMenu)
            {
                if (Main.myPlayer == player.whoAmI) // This client
                {
                    if (Main.MouseWorld.X > player.Center.X)
                    {
                        player.ChangeDir(1);
                    }
                    else
                    {
                        player.ChangeDir(-1);
                    }
                }
                player.itemLocation = player.MountedCenter + new Vector2(6 * player.direction, 4);
                if (Main.myPlayer == player.whoAmI) // This client
                {
                    Vector2 playerToCursor = (Main.MouseWorld - player.Center);
                    playerToCursor.Normalize();
                    player.itemRotation = playerToCursor.ToRotation() + (float)Math.PI / 4.0f;
                    Vector2 handleOffset = (new Vector2(4, -4)).RotatedBy(player.itemRotation);
                    player.itemLocation -= handleOffset;
                }
                else // For other clients
                {
                    player.itemRotation = itemRotation;
                    Vector2 handleOffset = (new Vector2(4, -4)).RotatedBy(player.itemRotation);
                    player.itemLocation -= handleOffset;
                }
                if (player.direction == -1)
                {
                    player.itemRotation += (float)Math.PI / 2.0f;
                }
            }
        }
        /// <summary>
        /// Time until a battery is removed from the player's inventory
        /// </summary>
        public int consumeBatteryTimer;
        public override void HoldItem(Player player)
        {
            Light(player);
            // Main.NewTextMultiline($"Light Percent: {lightPercent * 100}%\nLight Range: {lightRange} tiles");
            consumeBatteryTimer++;
            if (consumeBatteryTimer >= 1800 || player.CountItem(ModContent.ItemType<Battery>()) == 0)
            {
                player.ConsumeItem(ModContent.ItemType<Battery>());
                consumeBatteryTimer = 0;
            }
        }
        /// <summary>
        /// Distance that light travels away from the actual flashlight
        /// </summary>
        public int lightRange;
        /// <summary>
        /// The power of the light
        /// </summary>
        public float lightPercent;
        public void Light(Player player, bool notHoldingItem = false)
        {
            // TODO: Maybe later calculate lighting as a constant instead of changing light directly
            float numBatteries = player.CountItem(ModContent.ItemType<Battery>(), 5);
            lightPercent = numBatteries / 5;
            lightRange = (int)numBatteries * 10;
            for (int x = 0; x < lightRange; x++)
            {
                float lightItemRotation;
                if (notHoldingItem)
                {
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Vector2 playerToCursor2 = (Main.MouseWorld - player.Center);
                        playerToCursor2.Normalize();
                        lightItemRotation = playerToCursor2.ToRotation() + (float)Math.PI / 4.0f;
                    }
                    else
                    {
                        lightItemRotation = itemRotation;
                    }
                }
                else
                {
                    lightItemRotation = player.itemRotation;
                }
                Vector2 playerToCursor;
                if (notHoldingItem)
                {
                    playerToCursor = (lightItemRotation - (float)Math.PI / 4.0f).ToRotationVector2();
                }
                else
                {
                    playerToCursor = (lightItemRotation - (player.direction == -1 ? (float)Math.PI / 2.0f : 0) - (float)Math.PI / 4.0f).ToRotationVector2();
                }
                Vector2 position = player.itemLocation + playerToCursor * 16 * x;
                if (Collision.SolidCollision(position, 10, 10))
                {
                    lightPercent *= 0.8f;
                }
                Lighting.AddLight(position, new Vector3(1, 1, 1) / (x * 0.1f + 1) * 0.8f * lightPercent);
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ATest.Content.STAR
{
    public class STARProj : ModProjectile
    {
        public override string Texture => "ATest/Content/STAR/STARS";

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 10;
            Projectile.friendly = true;
            Projectile.width = 28;
            Projectile.height = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 1;
        }

        public static bool IsSouth = true;

        public override void OnSpawn(IEntitySource source)
        {
            if (!IsSouth)
            {
                Projectile.ai[0] = 1f;
            }
            else
            {
                Projectile.ai[0] = 2f;
            }

            Projectile.ai[1] = 1f;

            IsSouth = !IsSouth;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D STARNorth = ModContent.Request<Texture2D>("ATest/Content/STAR/STARN", AssetRequestMode.ImmediateLoad).Value;
            if (Projectile.ai[0] == 1f && Projectile.ai[0] != 2f)
            {
                Main.spriteBatch.Draw(STARNorth, Projectile.Center - Main.screenPosition,null,Color.White,Projectile.velocity.ToRotation(), STARNorth.Size()/2,1,SpriteEffects.None,0);
            }
            return Projectile.ai[0] != 1f;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.type == ModContent.ProjectileType<STARProj>() && target.active && projectile.ai[1] != 2f)
                {
                    projectile.ai[1] = 2f;
                    projectile.DirectionTo(target.position);
                    projectile.velocity = ((target.position - projectile.position) / 10);
                }
            }
            Projectile.Kill();
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2 acceleration = Projectile.velocity / 30;
            if (Projectile.ai[1] != 2f)
            {
                Projectile.velocity -= acceleration;
            }
            
        }
    }

}

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ATest.Content.STAR
{
    public class STAROnSpawn : ModProjectile
    {
        public override string Texture => "ATest/Content/STAR/STAR";

        public override void SetDefaults()
        {
            Projectile.damage = 10;
            Projectile.knockBack = 10f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Player player = Main.LocalPlayer;

            Vector2 direction = Main.MouseWorld - player.Center;
            //for (int i = -2; i < 2; i++)
            {
                int name = ModContent.ProjectileType<STARProj>();

                //float angle = (float)Math.Atan2(direction.Y, direction.X) + (i * float.Pi / 36f);
                Projectile.NewProjectile(source, Projectile.position, Projectile.velocity, name, Projectile.damage, Projectile.knockBack);
            }
            Projectile.Kill();
        }
    }
}

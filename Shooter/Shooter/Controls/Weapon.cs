﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Weapon {
        //Texture for displaying weapon in the HUD
        protected Texture2D texture;
        //texture for ammo
        protected Texture2D ammoTexture;

        //holds queued shots if clicked too early
        bool queued = false;
        //each int in the list signifies how full that magazine is
        protected List<int> ammo;
        public int maxAmmo;
        //if the gun is fully automatic
        protected bool auto;
        //randomness of the fire pattern
        protected double spread;
        //shots per second
        protected double fireRate;
        //Amount of time reload takes
        protected double reloadTime;
        //reload timer
        public double reloadTimer;
        public bool isReloading;
        //name of gun (gun type)
        protected string name;
        public double timeSinceLastShot = 1000;
        protected int ammoCount;
        //Weapon's damage
        protected int damage;
        //Weapon's range
        protected double range;
        //Weapon texture level
        protected int level;
        private Random r = new Random();
        //default weapon is a pistol
        public Weapon(ContentManager content) {
            auto = false;
            ammo = new List<int>();
            fireRate = 7;

            //randomness in fire pattern is a 10 degree arc
            spread = 10;
            name = "Pistol";
            maxAmmo = 9;
            texture = content.Load<Texture2D>("Pistol");
            ammoTexture = content.Load<Texture2D>("Ammo");
            ammoCount = 6;
            FillAmmo();
            isReloading = false;
            reloadTime = 1800.0;
            reloadTimer = 0;
            damage = 2;
            range = 7;
            level = 1;
        }

        public Weapon(ContentManager content, List<int> a, bool au, double spr, int fr, string t, int d) {
            fireRate = fr;
            ammo = a;
            auto = au;
            spread = spr;
            texture = content.Load<Texture2D>(t);
            isReloading = false;
            reloadTime = 1800.0;
            reloadTimer = 0;
            damage = d;
            range = 7;
            level = 1;
        }

        public Weapon(ContentManager content, bool au, double spr, int fr, string t, int d, string ammoT, string n, int maxAm, int ammoC, double rlTime, int rng, int lvl) {
            fireRate = fr;
            auto = au;
            spread = spr;
            texture = content.Load<Texture2D>(t);
            ammoTexture = content.Load<Texture2D>(ammoT);
            name = n;
            maxAmmo = maxAm;
            ammo = new List<int>();
            ammoCount = ammoC;
            FillAmmo();
            isReloading = false;
            reloadTime = rlTime;
            reloadTimer = 0;
            damage = d;
            range = rng;
            level = lvl;
        }

        public double FireRate {
            get { return fireRate; }
            set {fireRate = value;}
        }
        public bool Auto {
            get { return auto; }
        }
        public double Spread {
            get { return spread; }
        }
        public List<int> Ammo {
            get { return ammo; }
        }
        public string Name {
            get {
                return name;
            }
        }
        public int Damage {
            get { return damage; }
            set { damage = value; }
        }
        public bool Queued {
            get {
                return queued;
            }
            set {
                queued = value;
            }
        }
        public Texture2D Texture {
            get {
                return texture;
            }
        }
        public Texture2D AmmoTexture {
            get {
                return ammoTexture;
            }
        }

        public double ReloadTime {
            get { return reloadTime; }
            set { reloadTime = value; }
        }

        public int Level {
            get { return level; }
            set { level = value; }
        }
        public double GetSpread() {
            //returns a random integer that will be the offset of the bullet direction
            return r.Next((int)-spread / 2, (int)spread / 2);
        }
        public bool CheckAmmo() {
            if (ammo[0] <= 0) {
                return false;
            } else {
                return true;
            }
        }
        public virtual Projectile Shoot(ContentManager content, Character p, Camera c, int tileSize) {
            timeSinceLastShot = 0;
            if (CheckAmmo()) {
                Projectile proj = null;
                if (name.Equals("Rifle")) {
                    proj = new Projectile(content, p.Loc.X, p.Loc.Y, p.Direction + GetSpread() * (Math.PI / 180.0), 5, "BulletTwo", true,
                                                   new Rectangle((int)((c.camPos.X + p.Loc.X) * tileSize), (int)((c.camPos.Y + p.Loc.Y) * tileSize), tileSize, tileSize), range, true);
                } else if (name.Equals("Pistol")) {
                    proj = new Projectile(content, p.Loc.X, p.Loc.Y, p.Direction + GetSpread() * (Math.PI / 180.0), 5, "Bullet", true,
                                                   new Rectangle((int)((c.camPos.X + p.Loc.X) * tileSize), (int)((c.camPos.Y + p.Loc.Y) * tileSize), tileSize, tileSize), range, false);
                } else {
                    proj = new Projectile(content, p.Loc.X, p.Loc.Y, p.Direction + GetSpread() * (Math.PI / 180.0), 5, "BulletTwo", true,
                                                    new Rectangle((int)((c.camPos.X + p.Loc.X) * tileSize), (int)((c.camPos.Y + p.Loc.Y) * tileSize), tileSize, tileSize), range, false);
                }
                ammo[0]--;
                return proj;
            } else {
                return null;
            }
        }
        //Reloads gun
        public void Reload() {
            if (ammo.Count <= 1) {
                return;
            } else if (ammo[0] <= maxAmmo) {
                ammo.Remove(0);
                ammo.Sort();
                ammo.Reverse();
                isReloading = true;
            }
        }
        //Checks if gun is reloading
        public bool Reloading(double timeElapsed) {
            if (isReloading) {
                reloadTimer += timeElapsed;
            }
            if (reloadTime <= reloadTimer && isReloading) {
                //isReloading = false;
                reloadTimer = 0;
            }
            return isReloading;
        }
        public int TotalAmmo() {
            int total = 0;
            foreach (int i in ammo) {
                total += i;
            }
            return total;
        }
        //Checks if gun is ready to be fired agaim
        //takes an int (milliseconds) of the time since the last shot was fired
        public bool CheckFireRate(double timeElapsed) {
            timeSinceLastShot += timeElapsed;
            if (timeSinceLastShot > (1000 / fireRate)) {
                return true;
            } else {
                queued = true;
                return false;
            }
        }

        //Fills the weapon with ammo
        public void FillAmmo() {
            ammo.Clear();
            for (int i = 0; i < ammoCount; i++) {
                ammo.Add(maxAmmo);
            }
        }
    }
}

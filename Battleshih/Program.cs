using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

Raylib.InitWindow(800, 800, "Place Invaders"); //Create Window
Raylib.SetTargetFPS(60);
Raylib.SetExitKey(KeyboardKey.Escape); //Close Game on Esc.
float speed = 4; //Movement speed

Texture2D Ship = Raylib.LoadTexture(@"Ship.png"); //Skapa skepp
Rectangle SHitBox = new(400, 700, Ship.Dimensions);

Texture2D BulletTexture = Raylib.LoadTexture(@"Bullet.png"); //Create bullet
List<Bullet> Bullets = [];
List<Bullet> toRemove = [];

Texture2D EnemyTexture = Raylib.LoadTexture(@"SpaceInvader.png");
List<Enemy> Enemies = [];

for (int i = 1; i < 8; i++)
{
Enemy enemy = new()
{
    Position = new Vector2(i * 90, 80),
    Alive = true,
    EnemyRect = new(i * 90, 80, EnemyTexture.Dimensions),
};
Enemies.Add(enemy);

}



while (!Raylib.WindowShouldClose())
{

    if (Raylib.IsKeyDown(KeyboardKey.Right)) SHitBox.X += speed;  //Keyboard controls right
    if (Raylib.IsKeyDown(KeyboardKey.Left)) SHitBox.X -= speed;  //Keyboard controls left


    if (SHitBox.X > 750) //Out of bounds stopper right
    {
        SHitBox.X -= speed;
    }
    if (SHitBox.X < 0) //Out of bounds stopper left
    {
        SHitBox.X += speed;
    } 


    if (Raylib.IsKeyPressed(KeyboardKey.Space)) //Bullet creation on SPACE-click
    {
        Bullet b = new() //Create bullet after class
        {
            Velocity = new Vector2(0, -5), 
            Position = new Vector2(SHitBox.X+27, SHitBox.Y-13), //Spawns at the tip of the ship
            BulletBox = new(SHitBox.X+27, SHitBox.Y-13, BulletTexture.Dimensions),
        };
        Bullets.Add(b); // Add bullets to the list
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);


    foreach (Enemy enemies in Enemies) //Draw enemies to their location 
    {
    
        Raylib.DrawTexture(EnemyTexture, (int)enemies.Position.X, (int)enemies.Position.Y, Color.White);
        
    }
    
    
foreach (Bullet bullet in Bullets) //Every bullet in the bullets list moves according to the set velocity
{
    bullet.Position += bullet.Velocity;    //Move according to velocity
    bullet.BulletBox.Position += bullet.Velocity;
    Raylib.DrawTexture(BulletTexture, (int)bullet.Position.X, (int)bullet.Position.Y, Color.White); //Draw the bullet after changing its position
}

    Raylib.DrawTexture(Ship, (int)SHitBox.X, (int)SHitBox.Y, Color.White); //Draw the Ship/Player
    Raylib.EndDrawing();

    foreach (Bullet bullet in Bullets) //If a bullet goes offscreen, remove it from the list of bullets
    {
        if (bullet.Position.Y < 0) 
        {
            toRemove.Add(bullet);
        }
        
    }

    foreach (Bullet bullet in toRemove)
    {
        Bullets.Remove(bullet);
    }
}



class Bullet
{
public Vector2 Velocity;
public Vector2 Position;
public Rectangle BulletBox;
}

class Enemy
{
    public bool Alive;
    public Vector2 Position;

    public Rectangle EnemyRect;
}


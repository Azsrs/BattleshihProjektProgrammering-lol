using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

Raylib.InitWindow(800, 800, "Place Invaders"); //Create Window
Raylib.SetTargetFPS(60);
Raylib.SetExitKey(KeyboardKey.Escape); //Close Game on Esc.
float speed = 4; //Movement speed

bool winner = false;

Texture2D Ship = Raylib.LoadTexture(@"Ship.png"); //Skapa skepp
Rectangle SHitBox = new(400, 700, Ship.Dimensions);

Texture2D BulletTexture = Raylib.LoadTexture(@"Bullet.png"); //Create bullet
List<Bullet> Bullets = [];
List<Bullet> toRemoveB = [];

Texture2D EnemyTexture = Raylib.LoadTexture(@"SpaceInvader.png"); //Create enemies
List<Enemy> Enemies = [];
List<Enemy> ToRemoveE = [];

for (int i = 1; i < 8; i++) //Create enemies in a row after enemy class, add to enemy list
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
    if (Enemies.Count == 0) //Victory
    {
        winner = true;
    }

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
            Position = new Vector2(SHitBox.X + 27, SHitBox.Y - 13), //Spawns at the tip of the ship
            BulletBox = new(SHitBox.X + 27, SHitBox.Y - 13, BulletTexture.Dimensions),
        };
        Bullets.Add(b); // Add bullets to the list
    }

    if (winner == true)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        
        Raylib.DrawText("Congratulations!!", 150, 400, 50, Color.White);
        Raylib.EndDrawing();
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            Raylib.CloseWindow();
        }
    }
    if (winner == false) {
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


    foreach (Bullet bullet in Bullets)
    {
        if (bullet.Position.Y < 0) //If a bullet goes offscreen, remove it from the list of bullets
        {
            toRemoveB.Add(bullet);
        }
        foreach (Enemy enemies in Enemies)
        {
            if (Raylib.CheckCollisionRecs(bullet.BulletBox, enemies.EnemyRect)) //If a bullet hits an enemy, remove the bullet and the enemy
            {
                toRemoveB.Add(bullet);
                ToRemoveE.Add(enemies);
            }
        }
    }

    foreach (Bullet bullet in toRemoveB) //Removes unneeded bullets
    {
        Bullets.Remove(bullet);
    }
    foreach (Enemy enemies in ToRemoveE) //Removes hit enemies 
    {
        Enemies.Remove(enemies);
    }

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


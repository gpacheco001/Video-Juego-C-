﻿using NaveEspacial;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

//Console.WriteLine("Ancho maximo: "+ Console.LargestWindowWidth);
//Console.WriteLine("Altura maxima: " + Console.LargestWindowHeight);

Ventana ventana;
Nave nave;
Enemigo enemigo1;
Enemigo enemigo2;
Enemigo enemigoBoss;
bool bossFinal = false;
bool jugar = false;
bool ejecucion = true;

void Iniciar() 
{
    ventana = new Ventana(150, 40, ConsoleColor.Black, new Point(5, 5), new Point(145, 35));
    ventana.DibujarMarco();
    nave = new Nave(new Point(70,30), ConsoleColor.White, ventana);
    //nave.Dibujar();
    enemigo1 = new Enemigo(new Point(50,10), ConsoleColor.Cyan, ventana, TipoEnemigo.Normal,
        nave);
    //enemigo1.Dibujar();
    enemigo2 = new Enemigo(new Point(90, 10), ConsoleColor.Red, ventana, TipoEnemigo.Normal,
        nave);    
    //enemigo2.Dibujar();
    enemigoBoss = new Enemigo(new Point(100, 10), ConsoleColor.Magenta, ventana, TipoEnemigo.Boss,
        nave);

    nave.Enemigos.Add(enemigo1);
    nave.Enemigos.Add(enemigo2);
    nave.Enemigos.Add(enemigoBoss);
}
void Reiniciar()
{
    Console.Clear();
    ventana.DibujarMarco();

    nave.Vida = 100;
    nave.SobreCarga = 0;
    nave.BalaEspecial = 0;
    nave.Balas.Clear();

    enemigo1.Vida = 100;
    enemigo1.Vivo = true;
    enemigo2.Vida = 100;
    enemigo2.Vivo = true;
    enemigoBoss.Vida = 100;
    enemigoBoss.Vivo = true;
    enemigoBoss.PosicionesEnemigo.Clear();

    bossFinal = false;
}
void Game() 
{
    while (ejecucion)
    {
        ventana.Menu();
        ventana.Teclado(ref ejecucion, ref jugar);

        while (jugar)
        {
            if (!enemigo1.Vivo && !enemigo2.Vivo && !bossFinal)
            {
                bossFinal = true;
                ventana.Peligro();
            }
            if (bossFinal)
            {
                enemigoBoss.Mover();
                enemigoBoss.Informacion(110);
            }
            else
                enemigo1.Mover();
                enemigo1.Informacion(70);
                enemigo2.Mover();
                enemigo2.Informacion(90);

            nave.Mover(2);
            nave.Disparar();

            if (nave.Vida <= 0)
            {
                jugar = false;
                nave.Muerte();
                Reiniciar();
            }

            if (!enemigoBoss.Vivo)
            {
                jugar = false;
                Reiniciar();
            }
        }
    }        
}

Iniciar();
Game();
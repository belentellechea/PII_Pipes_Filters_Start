using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using System.Data.Common;
using SixLabors.ImageSharp;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {

        //EJERCICIO 1  
            //cargar la foto original
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture (@"C:\Users\telle\OneDrive\Escritorio\programacion\PII_Pipes_Filters_Start\src\Program\beer.jpg");

            //crear los filtros
            IFilter filterGreyscale = new FilterGreyscale();
            IFilter filterNegative = new FilterNegative();

            //crear los pipes en orden inverso
            PipeNull pipeNull = new PipeNull();
            PipeSerial pipeSerial2 = new PipeSerial (filterNegative, pipeNull);
            PipeSerial pipeSerial1 = new PipeSerial (filterGreyscale, pipeSerial2);

            //guardar la foto final
            IPicture finalPicture = pipeSerial1.Send(picture);
            provider.SavePicture(finalPicture, @"C:\Users\telle\OneDrive\Escritorio\programacion\PII_Pipes_Filters_Start\src\Program\filterbeer.jpg");


        //EJERCICIO 2  
            PictureProvider provider1 = new PictureProvider();
            IPicture originalImage = provider1.GetPicture(@"C:\Users\telle\OneDrive\Escritorio\programacion\PII_Pipes_Filters_Start\src\Program\luke.jpg");

            IFilter filterNegative1 = new FilterGreyscale();
            IFilter filterGreyscale1 = new FilterNegative();
            
            string intermediatePath = @"C:\Users\telle\OneDrive\Escritorio\programacion\PII_Pipes_Filters_Start\src\Program\intermediateluke.jpg";
            IFilter saveIntermediateFilter = new TransformationStep(intermediatePath, provider);

            // Aplicación de los filtros.
            IPicture negativeImage = filterNegative.Filter(originalImage);
            IPicture savedNegativeImage = saveIntermediateFilter.Filter(negativeImage); // Guarda y retorna la imagen intermedia.
            IPicture greyscaleImage = filterGreyscale.Filter(savedNegativeImage);

            // Guardado de la imagen final.
            string finalPath = @"C:\Users\telle\OneDrive\Escritorio\programacion\PII_Pipes_Filters_Start\src\Program\lukefinal.jpg";
            provider.SavePicture(greyscaleImage, finalPath);

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MathsLibrary
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class MathsOperations : IMathsOperations
    {
        public int Add(int valueA, int valueB)
        {
            return valueA + valueB;
        }

        public int Multiply(int valueA, int valueB)
        {
            return valueA * valueB;
        }

        public int Subtract(int valueA, int valueB)
        {
            return valueA - valueB;
        }

    }
}

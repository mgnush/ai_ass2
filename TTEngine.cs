﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class TTEngine : Engine
    {
        public TTEngine(string file) : base(file)
        {

        }

        /* Checks the value of the query in the current model
         * @return Whether the query is true
         */
        private bool TTQuery()
        {
            foreach(Symbol symbol in _kb.Symbols)
            {
                if (symbol.Name.Equals(_query.Name))
                {
                    return symbol.Value;
                }
            }
            return false;
        }

        /* Checks whether every sentence in the kb is satisfied.
         * Uses the current value of all symbols in the model
         * @return whether every sentence in kb is satisfied 
         */
        private bool TTCheckAll()
        {
            foreach(Sentence sentence in _kb.Sentences)
            {
                if (!sentence.IsSatisfied(_kb.Symbols))
                {
                    return false;
                }
            }

            return true; // Return true if all sentences hold

        }

        public override string KbEntails()
        {
            // Iterate through models (BFS)
            long numberModels = (long)Math.Pow(2, _kb.Symbols.Count);
            long trueModels = 0;
            long validModels = 0;

            // Count the number of models that are true/entailed
            for(long i = 0; i < numberModels; i++)
            {
                string model = Convert.ToString(i, 2).PadLeft(_kb.Symbols.Count, '0');
                // Build model by setting the P values
                for(int j = 0; j < _kb.Symbols.Count; j++)
                {
                    _kb.Symbols.ElementAt(j).Value = (model[j] == '1');
                }

                // Check if KB => query
                if (!(TTCheckAll() & !TTQuery())) { validModels++; }
                // Check if KB^query
                if (TTCheckAll() && TTQuery()) { trueModels++; }
            }

            // KB entails query if (KB => query) is valid in all models
            if (validModels == numberModels)
            {
                string output = "YES: ";
                output += trueModels.ToString();
                return output;                
            } else
            {
                return "NO";
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class KnowledgeBase
    {
        private List<Sentence> _sentences;
        private List<Symbol> _symbols;

        public KnowledgeBase(string[] sentences)
        {
            _sentences = new List<Sentence>();
            _symbols = new List<Symbol>();

            foreach(string s in sentences)
            {
                string[] stringSeparators = new string[] { "=>", "\r" };
                string[] vs = s.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                Sentence sentence;

                // Build sentence according to number of sides (0, 1 or 2)
                switch(vs.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        sentence = new Sentence(vs[0]);
                        _sentences.Add(sentence);
                        break;
                    default:
                        sentence = new Sentence(vs[0], vs[1]);
                        _sentences.Add(sentence);
                        break;
                }

                // Add new symbols to list of symbols in kb
                foreach(Symbol symbol in sentence.AllSymbols)
                {
                    bool newSymbol = true;
             
                    foreach (Symbol kbs in _symbols)
                    {
                        if (symbol.Name.Equals(kbs.Name))
                        {
                            newSymbol = false;
                            break;
                        }
                    }
                    if (newSymbol) { _symbols.Add(symbol); }
                    
                }
            }
        }

        public List<Symbol> Symbols { get => _symbols; }
        public List<Sentence> Sentences { get => _sentences; }
    }
}

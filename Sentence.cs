﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class Sentence
    {
        private List<Symbol> _lhs;
        private Symbol _rhs;
        private List<Symbol> _allSymbols;

        // Constructor for sentences with no premises
        public Sentence(string rhs)
        {
            _allSymbols = new List<Symbol>();
            _lhs = new List<Symbol>();                  

            _rhs = new Symbol(rhs.Trim());
            _allSymbols.Add(_rhs);           
        }

        //Constructor for sentences with two sides
        public Sentence(string lhs, string rhs)
        {
            _allSymbols = new List<Symbol>();
            _lhs = new List<Symbol>();

            // RHS
            _rhs = new Symbol(rhs.Trim());
            _allSymbols.Add(_rhs);

            // LHS 
            string[] lh = lhs.Split('&');
            foreach (string symbol in lh)
            {
                _lhs.Add(new Symbol(symbol.Trim()));
            }
            _allSymbols.AddRange(_lhs);
    }

        public List<Symbol> AllSymbols { get => _allSymbols; }
        public List<Symbol> Premises { get => _lhs; }
        public Symbol Head { get => _rhs; }

        public bool ContainsPremise(Symbol search)
        {
            return _lhs.Exists(x => x.Name.Equals(search.Name));
        }

        /* Return whether this sentence is satisfied given 
         * a model of the world.
         * @param model the list of symbols (with values) in the model
         * @return Whether the sentence is satisfied in given model  
         */
        public bool IsSatisfied(List<Symbol> model)
        {
            // Obtain symbol values from model
            foreach (Symbol s in model)
            {
                foreach (Symbol ls in _lhs)
                {
                    if (s.Name.Equals(ls.Name))
                    {
                        ls.Value = s.Value;
                    }
                }
                if (s.Name.Equals(_rhs.Name))
                {
                    _rhs.Value = s.Value;
                }
            }

            bool lhs = true;
            foreach (Symbol ls in _lhs)
            {
                lhs &= ls.Value;
            }

            return !(lhs & !_rhs.Value); // The sentence is satisfied unless the premises are true and head is false
        }
    }
}

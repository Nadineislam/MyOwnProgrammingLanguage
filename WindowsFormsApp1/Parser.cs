
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF              =  0, // (EOF)
        SYMBOL_ERROR            =  1, // (Error)
        SYMBOL_WHITESPACE       =  2, // Whitespace
        SYMBOL_MINUS            =  3, // '-'
        SYMBOL_MINUSMINUS       =  4, // '--'
        SYMBOL_EXCLAMEQ         =  5, // '!='
        SYMBOL_LPAREN           =  6, // '('
        SYMBOL_RPAREN           =  7, // ')'
        SYMBOL_TIMES            =  8, // '*'
        SYMBOL_TIMESTIMES       =  9, // '**'
        SYMBOL_DOTDOT           = 10, // '..'
        SYMBOL_DIV              = 11, // '/'
        SYMBOL_COLON            = 12, // ':'
        SYMBOL_LBRACKET         = 13, // '['
        SYMBOL_RBRACKET         = 14, // ']'
        SYMBOL_PLUS             = 15, // '+'
        SYMBOL_PLUSPLUS         = 16, // '++'
        SYMBOL_LT               = 17, // '<'
        SYMBOL_EQ               = 18, // '='
        SYMBOL_EQEQ             = 19, // '=='
        SYMBOL_GT               = 20, // '>'
        SYMBOL_BREAK            = 21, // break
        SYMBOL_CASE             = 22, // case
        SYMBOL_DIGIT            = 23, // Digit
        SYMBOL_ELSE             = 24, // else
        SYMBOL_END              = 25, // End
        SYMBOL_FOR              = 26, // for
        SYMBOL_ID               = 27, // Id
        SYMBOL_IF               = 28, // if
        SYMBOL_IN               = 29, // in
        SYMBOL_START            = 30, // Start
        SYMBOL_SWITCH           = 31, // switch
        SYMBOL_VAL              = 32, // val
        SYMBOL_ASSIGN           = 33, // <assign>
        SYMBOL_CONTENT          = 34, // <content>
        SYMBOL_DECLARE          = 35, // <declare>
        SYMBOL_DIGIT2           = 36, // <digit>
        SYMBOL_EXP              = 37, // <exp>
        SYMBOL_EXPR             = 38, // <expr>
        SYMBOL_FACTOR           = 39, // <factor>
        SYMBOL_FOR_STMT         = 40, // <for_stmt>
        SYMBOL_ID2              = 41, // <id>
        SYMBOL_IF_STMT          = 42, // <if_stmt>
        SYMBOL_OPERATION        = 43, // <operation>
        SYMBOL_PROGRAM_LANGUAGE = 44, // <program_language>
        SYMBOL_STMT_LIST        = 45, // <stmt_list>
        SYMBOL_SWITCH_STMT      = 46, // <switch_stmt>
        SYMBOL_TERM             = 47, // <term>
        SYMBOL_TEST             = 48, // <test>
        SYMBOL_UPDATE_STMT      = 49  // <update_stmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_LANGUAGE_START_END                                                                  =  0, // <program_language> ::= Start <stmt_list> End
        RULE_STMT_LIST                                                                                   =  1, // <stmt_list> ::= <content>
        RULE_STMT_LIST2                                                                                  =  2, // <stmt_list> ::= <content> <stmt_list>
        RULE_CONTENT                                                                                     =  3, // <content> ::= <declare>
        RULE_CONTENT2                                                                                    =  4, // <content> ::= <assign>
        RULE_CONTENT3                                                                                    =  5, // <content> ::= <if_stmt>
        RULE_CONTENT4                                                                                    =  6, // <content> ::= <switch_stmt>
        RULE_CONTENT5                                                                                    =  7, // <content> ::= <for_stmt>
        RULE_DECLARE_VAL_EQ_COLON                                                                        =  8, // <declare> ::= val <id> '=' <expr> ':'
        RULE_ID_ID                                                                                       =  9, // <id> ::= Id
        RULE_EXPR_PLUS                                                                                   = 10, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                                                                  = 11, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                                                        = 12, // <expr> ::= <term>
        RULE_TERM_TIMES                                                                                  = 13, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                                                    = 14, // <term> ::= <term> '/' <factor>
        RULE_TERM                                                                                        = 15, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                                                           = 16, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                                                                      = 17, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                                                           = 18, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                                                         = 19, // <exp> ::= <digit>
        RULE_EXP2                                                                                        = 20, // <exp> ::= <id>
        RULE_DIGIT_DIGIT                                                                                 = 21, // <digit> ::= Digit
        RULE_ASSIGN_EQ_COLON                                                                             = 22, // <assign> ::= <id> '=' <expr> ':'
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET                                                  = 23, // <if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']'
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET_ELSE_IF_LBRACKET_RBRACKET                        = 24, // <if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']' else if '[' <stmt_list> ']'
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET_ELSE_IF_LBRACKET_RBRACKET_ELSE_LBRACKET_RBRACKET = 25, // <if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']' else if '[' <stmt_list> ']' else '[' <stmt_list> ']'
        RULE_TEST                                                                                        = 26, // <test> ::= <expr> <operation> <expr>
        RULE_OPERATION_GT                                                                                = 27, // <operation> ::= '>'
        RULE_OPERATION_LT                                                                                = 28, // <operation> ::= '<'
        RULE_OPERATION_EQEQ                                                                              = 29, // <operation> ::= '=='
        RULE_OPERATION_EXCLAMEQ                                                                          = 30, // <operation> ::= '!='
        RULE_FOR_STMT_FOR_IN_DOTDOT_LBRACKET_RBRACKET                                                    = 31, // <for_stmt> ::= for <id> in <digit> '..' <digit> '[' <stmt_list> ']'
        RULE_FOR_STMT_FOR_LPAREN_IN_RPAREN_LBRACKET_RBRACKET                                             = 32, // <for_stmt> ::= for '(' <id> in <id> ')' '[' <stmt_list> ']'
        RULE_FOR_STMT_FOR_LPAREN_COLON_COLON_RPAREN_LBRACKET_RBRACKET                                    = 33, // <for_stmt> ::= for '(' <declare> ':' <test> ':' <update_stmt> ')' '[' <stmt_list> ']'
        RULE_UPDATE_STMT_PLUSPLUS                                                                        = 34, // <update_stmt> ::= '++' <id>
        RULE_UPDATE_STMT_MINUSMINUS                                                                      = 35, // <update_stmt> ::= '--' <id>
        RULE_UPDATE_STMT_PLUSPLUS2                                                                       = 36, // <update_stmt> ::= <id> '++'
        RULE_UPDATE_STMT_MINUSMINUS2                                                                     = 37, // <update_stmt> ::= <id> '--'
        RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACKET_CASE_COLON_RBRACKET_BREAK_COLON                   = 38, // <switch_stmt> ::= switch '(' <id> ')' '[' case <id> ':' <stmt_list> ']' break ':'
        RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACKET_CASE_COLON_RBRACKET_BREAK_COLON2                  = 39  // <switch_stmt> ::= switch '(' <id> ')' '[' case <id> ':' <stmt_list> ']' break ':' <switch_stmt>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox list;

        public MyParser(string filename,ListBox list)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.list = list;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOTDOT :
                //'..'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAK :
                //break
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VAL :
                //val
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONTENT :
                //<content>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARE :
                //<declare>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPERATION :
                //<operation>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM_LANGUAGE :
                //<program_language>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TEST :
                //<test>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UPDATE_STMT :
                //<update_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_LANGUAGE_START_END :
                //<program_language> ::= Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <content>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <content> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTENT :
                //<content> ::= <declare>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTENT2 :
                //<content> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTENT3 :
                //<content> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTENT4 :
                //<content> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTENT5 :
                //<content> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARE_VAL_EQ_COLON :
                //<declare> ::= val <id> '=' <expr> ':'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_COLON :
                //<assign> ::= <id> '=' <expr> ':'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET :
                //<if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET_ELSE_IF_LBRACKET_RBRACKET :
                //<if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']' else if '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACKET_RBRACKET_ELSE_IF_LBRACKET_RBRACKET_ELSE_LBRACKET_RBRACKET :
                //<if_stmt> ::= if '(' <test> ')' '[' <stmt_list> ']' else if '[' <stmt_list> ']' else '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TEST :
                //<test> ::= <expr> <operation> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_GT :
                //<operation> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_LT :
                //<operation> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EQEQ :
                //<operation> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EXCLAMEQ :
                //<operation> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_IN_DOTDOT_LBRACKET_RBRACKET :
                //<for_stmt> ::= for <id> in <digit> '..' <digit> '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_IN_RPAREN_LBRACKET_RBRACKET :
                //<for_stmt> ::= for '(' <id> in <id> ')' '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_COLON_COLON_RPAREN_LBRACKET_RBRACKET :
                //<for_stmt> ::= for '(' <declare> ':' <test> ':' <update_stmt> ')' '[' <stmt_list> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UPDATE_STMT_PLUSPLUS :
                //<update_stmt> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UPDATE_STMT_MINUSMINUS :
                //<update_stmt> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UPDATE_STMT_PLUSPLUS2 :
                //<update_stmt> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UPDATE_STMT_MINUSMINUS2 :
                //<update_stmt> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACKET_CASE_COLON_RBRACKET_BREAK_COLON :
                //<switch_stmt> ::= switch '(' <id> ')' '[' case <id> ':' <stmt_list> ']' break ':'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACKET_CASE_COLON_RBRACKET_BREAK_COLON2 :
                //<switch_stmt> ::= switch '(' <id> ')' '[' case <id> ':' <stmt_list> ']' break ':' <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" in line:" + args.UnexpectedToken.Location.LineNr;
            list.Items.Add(message);
            string message2 ="Expected token is: " + args.ExpectedTokens.ToString();
            list.Items.Add(message2);

            //todo: Report message to UI?
        }

    }
}

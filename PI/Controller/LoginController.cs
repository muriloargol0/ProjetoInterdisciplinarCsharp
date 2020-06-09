﻿using PI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Controller
{
    class LoginController
    {
        public bool DoLogin(string login, string senha, out string mensagem)
        {
            using (var ctx = new DBContext())
            {
                var usr = (from u in ctx.USER
                           where u.LOGIN == login && u.SENHA == senha
                           select u).FirstOrDefault();

                if(usr == null)
                {
                    mensagem = "Usuário e senha incorretos!";
                    return false;
                }

                mensagem = string.Empty;
                return true;
            }
        }
    }
}
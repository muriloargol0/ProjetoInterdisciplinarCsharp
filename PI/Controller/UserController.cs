﻿using PI.Database;
using PI.Model.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PI.Controller
{
    public class UserController
    {

        public bool isFormCadastroOpened { get; set; }
        public bool isFormBuscaOpened { get; set; }
        public List<UserDTO> GetUsuarios(string parametro = "")
        {
            using (var ctx = new DBContext())
            {
                List<USER> usr = null;

                List<UserDTO> ListaUsuarios = new List<UserDTO>();

                if (string.IsNullOrEmpty(parametro))
                {
                    usr = (from user in ctx.USER
                           where user.ID_STATUS == 1
                           select user).ToList();
                }
                else
                {
                    usr = (from user in ctx.USER
                           where user.LOGIN == parametro
                           select user).ToList();
                }

                if (usr != null)
                {
                    foreach (var item in usr)
                    {
                        UserDTO u = new UserDTO();
                        u.idUser = item.ID_USER;
                        u.nome = item.NOME;
                        u.email = item.EMAIL;
                        u.login = item.LOGIN;
                        u.senha = item.SENHA;
                        u.id_status = item.ID_STATUS;

                        ListaUsuarios.Add(u);
                    }
                }

                return ListaUsuarios;
            }
        }

        public bool VerificaSeExiste(string name, string value, int ID)
        {
            using (var ctx = new DBContext())
            {
                USER query = null;

                if(name == "email")
                {
                    query = ctx.USER.Where(x => x.EMAIL == value).FirstOrDefault();

                    if(query.ID_USER != ID)
                    {
                        if (!string.IsNullOrEmpty(query.EMAIL.ToString()))
                            return true;
                    }
                }

                if(name == "login")
                {
                    query = ctx.USER.Where(x => x.LOGIN == value).FirstOrDefault();

                    if (query.ID_USER != ID)
                    {
                        if (!string.IsNullOrEmpty(query.LOGIN.ToString()))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}

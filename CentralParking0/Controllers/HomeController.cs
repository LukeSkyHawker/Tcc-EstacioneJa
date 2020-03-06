using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentralParking0.Models;
using System.ComponentModel.DataAnnotations;

namespace CentralParking0.Controllers
{
    public class HomeController : Controller
    {
        CentralParkingEntities db = new CentralParkingEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include ="Nome_Dono,email," +
            "Conta_Corrente,CPF,usuario,senha")]Locador usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Locadors.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                return View("Cadastrar");
            }
            catch
            {
                return View("Cadastrar");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Locador usuario)
        {
            if (ModelState.IsValid)
            {

                var v = db.Locadors.Where(a => a.usuario.Equals(usuario.usuario) &&
                a.senha.Equals(usuario.senha)).FirstOrDefault();
                if (v != null)
                {
                    if (v.Tipo.Equals("A"))
                    {
                        Session["nomeUsuarioLogado"] = v.Nome_Dono.ToString();
                        Session["login"] = v.usuario.ToString();
                        return RedirectToAction("Index", "Locadors");
                    }
                    if (v.Tipo.Equals("U"))
                    {
                        Session["nomeUsuarioLogado"] = v.Nome_Dono.ToString();
                        return RedirectToAction("Edit", "Locadors");
                    }
                }
                else
                {
                    ViewBag.msg = "<script>" +
                        "alert('Usuário ou Senha inválidos');" +
                        "</script>";
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View(usuario);
        }
    }
}
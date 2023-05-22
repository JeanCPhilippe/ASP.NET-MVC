using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService; 
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        //Método Index que mostra a tela de Vendedores quando acessado (Assincrono) 
        public async Task<IActionResult> Index()
        {
            //Lista que contém todos os Vendedores
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }
        //Método Create que mostra a tela de cadastro de vendedores (Assincrono) 
        public async Task<IActionResult> Create()
        {
            //Váriavel que recebe os dados de departamentos do Banco de Dados
            var departments = await _departmentService.FindAllAsync();
            //Método que facilita a impressão e escolha dos Departamentos
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Método post, ou seja, faz as modificações após a requisição
        public async Task<IActionResult> Create(Seller seller)
        {
            //Verificação de Requisição através do Código, caso o Javascript esteja desabilitado
            if (!ModelState.IsValid)
            {
                //Método para obter os departamentos e mostrá-los em lista
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            //Inserindo Vendedor no Banco de Dados
            await _sellerService.InsertSellerAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            //Verificando se foi recebida uma ID
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            //Verificando se a ID existe
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Método post, ou seja, faz as modificações após a requisição
        public async Task<IActionResult> Delete(int id)
        {
            //Removendo Vendedor
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            } catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            
        }
        public async Task<IActionResult> Details (int? id)
        {
            //Verificando se foi recebida uma ID
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            //Verificando se a ID existe
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //Verificando se foi recebida uma ID
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            //Verificando se a ID existe
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //Buscando e listando os departamentos
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel {Seller  = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Seller seller)
        {
            //Verificação de Requisição através do Código, caso o Javascript esteja desabilitado
            if (!ModelState.IsValid)
            {
                //Vai manter o site em looping até que a requisição seja aprovada.
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            //Verifica se há alguma inconsistência nas ID's
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                //Atualiza o Vendedor
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }
    }
}

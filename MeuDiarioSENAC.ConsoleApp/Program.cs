using MeuDiarioSENAC.ConsoleApp;
using MeuDiarioSENAC.Service;
using MeuDiarioSENAC.Service.Interfaces;
using MeuDiarioSENAC.Service.Services;

IRegistroService registroService = new RegistroService();
Menu menu = new(registroService);
menu.Executar();
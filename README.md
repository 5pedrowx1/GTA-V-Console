# GTA V Script Injector v2.0

## 📋 Descrição

Injetor automático para ScriptHookV e ScriptHookVDotNet com interface gráfica moderna e sistema de perfis personalizáveis.

## 🚀 Recursos

### Principais Funcionalidades
- ✅ Injeção automática ao detectar o GTA V
- ✅ Sistema de perfis personalizáveis
- ✅ Suporte para DLLs customizadas
- ✅ Verificação de modo online (proteção contra ban)
- ✅ Interface moderna e personalizável
- ✅ Sistema de logs detalhado
- ✅ Verificação de integridade de arquivos
- ✅ Suporte para 32-bit e 64-bit

### Arquitetura Refatorada
- **InjectionManager**: Gerencia todo o processo de injeção
- **ProcessMonitor**: Monitora e detecta o processo do jogo
- **LogManager**: Sistema de logs com salvamento em arquivo
- **SecurityUtils**: Verificações de segurança
- **UpdateChecker**: Verifica atualizações automaticamente

## 📦 Estrutura do Projeto

```
Injector UI/
├── Core/
│   ├── InjectionManager.cs          # Gerenciador de injeção
│   ├── ProcessMonitor.cs            # Monitor de processos
│   ├── LogManager.cs                # Gerenciador de logs
│   ├── ILogger.cs                   # Interface de logger
│   ├── FormLogger.cs                # Logger para Windows Forms
│   ├── SecurityUtils.cs             # Utilitários de segurança
│   ├── ProcessUtils.cs              # Utilitários de processo
│   ├── FileUtils.cs                 # Utilitários de arquivo
│   ├── Win32Api.cs                  # Interoperabilidade Win32
│   └── Win32Constants.cs            # Constantes Win32
├── ConfigManager.cs                 # Gerenciador de configurações
├── Form1.cs                         # Formulário principal
├── SettingsForm.cs                  # Formulário de configurações
├── InputForm.cs                     # Formulário de input
├── AddCustomDllForm.cs              # Formulário para adicionar DLLs
└── AboutForm.cs                     # Formulário sobre

```

## 🔧 Configuração

### config.json

O aplicativo cria automaticamente um arquivo `config.json` com as seguintes seções:

#### General (Geral)
```json
{
  "processName": "GTA5",
  "autoInject": true,
  "startMinimized": false,
  "minimizeToTray": true,
  "runOnStartup": false,
  "checkForUpdates": true,
  "saveLogs": true,
  "logDirectory": "Logs"
}
```

#### Injection (Injeção)
```json
{
  "processCheckInterval": 2000,
  "initializationTimeout": 30000,
  "injectionTimeout": 10000,
  "maxRetries": 3,
  "retryDelay": 2000,
  "waitForGameLoad": true,
  "gameLoadTimeout": 60
}
```

#### Interface
```json
{
  "theme": "Dark",
  "accentColor": "Purple",
  "showDetailedLogs": true,
  "showTimestamps": true,
  "fontSize": 9,
  "alwaysOnTop": false,
  "showTrayNotifications": true
}
```

#### Security (Segurança)
```json
{
  "warnOnlineMode": true,
  "verifyDllSignatures": false,
  "safeMode": false,
  "requireAdminPrivileges": true
}
```

## 📖 Como Usar

### Primeira Execução

1. **Execute como Administrador** (recomendado)
2. O aplicativo criará automaticamente o arquivo `config.json`
3. Configure suas preferências em **Configurações**

### Injeção Automática

1. Inicie o injetor
2. Aguarde o aplicativo detectar o GTA V
3. A injeção será feita automaticamente
4. Verifique os logs para confirmação

### Injeção Manual

1. Inicie o GTA V
2. Abra o injetor
3. Clique em **Injetar**
4. Aguarde a conclusão

### Criando Perfis

1. Acesse **Configurações > Perfis**
2. Clique em **Novo Perfil**
3. Configure as opções desejadas:
   - Injetar ScriptHookV
   - Injetar ScriptHookDotNet
   - Habilitar DLLs customizadas
4. Salve e selecione o perfil ativo

### Adicionando DLLs Customizadas

1. Acesse **Configurações > DLLs Customizadas**
2. Clique em **Adicionar**
3. Preencha as informações:
   - Nome da DLL
   - Caminho do arquivo
   - Descrição (opcional)
   - Prioridade (ordem de injeção)
4. Salve

## ⚙️ Requisitos

### Sistema
- Windows 7 ou superior
- .NET 6.0 ou superior
- GTA V (versão compatível com ScriptHookV)

### Dependências
- ScriptHookV.dll
- ScriptHookVDotNet (opcional)
- Privilégios de administrador (recomendado)

## 🛡️ Segurança

### Proteção Contra Ban

O aplicativo possui verificação de modo online:
- Detecta processos do Social Club
- Avisa o usuário antes de injetar
- Recomenda uso apenas em Story Mode

### Verificação de Integridade

- Verifica tamanho mínimo dos arquivos
- Checa informações de versão
- Valida arquitetura (32/64-bit)

## 📊 Sistema de Logs

### Tipos de Log
- **Info**: Informações gerais
- **Success**: Operações bem-sucedidas
- **Warning**: Avisos importantes
- **Error**: Erros e falhas

### Salvamento Automático
- Logs são salvos na pasta `Logs/`
- Formato: `injector_YYYYMMDD_HHMMSS.log`
- Logs de erro: `error_YYYYMMDD_HHMMSS.log`
- Limpeza automática após 7 dias

## 🎨 Temas

### Temas Disponíveis
- **Dark** (padrão)
- **Light**

### Cores de Destaque
- Purple (padrão)
- Blue
- Green
- Red
- Orange

## 🔍 Solução de Problemas

### "Acesso Negado"
- Execute como Administrador
- Desative temporariamente o antivírus
- Adicione exceção no Windows Defender

### "Processo não encontrado"
- Certifique-se de que o GTA V está rodando
- Verifique o nome do processo em **Configurações**
- Aguarde o jogo carregar completamente

### "Incompatibilidade de Arquitetura"
- Use a versão correta do injetor (32 ou 64-bit)
- Verifique se o GTA V é 64-bit (versão atual)

### "ScriptHookV não encontrado"
1. Baixe ScriptHookV de: http://www.dev-c.com/gtav/scripthookv/
2. Extraia `ScriptHookV.dll` para a pasta do GTA V
3. Reinicie o injetor

### "DLL não carregada"
- Aguarde mais tempo para inicialização
- Verifique se o arquivo existe
- Confirme permissões de leitura
- Tente reiniciar o jogo

## 🔄 Atualizações

O aplicativo verifica automaticamente por atualizações ao iniciar (pode ser desabilitado nas configurações).

## ⚠️ Aviso Legal

Este software é fornecido "como está", sem garantias de qualquer tipo. O uso de mods pode violar os termos de serviço do GTA Online. Use por sua conta e risco e **APENAS EM MODO STORY**.

## 📝 Changelog

### v2.0.0 (Atual)
- ✨ Arquitetura completamente refatorada
- ✨ Sistema de perfis personalizáveis
- ✨ Suporte para DLLs customizadas
- ✨ Interface modernizada
- ✨ Sistema de logs aprimorado
- ✨ Melhor detecção de processos
- ✨ Verificação de modo online
- ✨ Sistema de temas

### v1.0.0
- 🎉 Versão inicial

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir novas funcionalidades
- Enviar pull requests

## 📄 Licença

Este projeto é disponibilizado sob a licença MIT.

## 👨‍💻 Autor

Desenvolvido com ❤️ para a comunidade de modding do GTA V.

## 🔗 Links Úteis

- [ScriptHookV](http://www.dev-c.com/gtav/scripthookv/)
- [ScriptHookVDotNet](https://github.com/crosire/scripthookvdotnet)
- [GTA5-Mods](https://www.gta5-mods.com/)

---

**⚠️ IMPORTANTE**: Use mods apenas em modo offline (Story Mode). O uso de mods no GTA Online pode resultar em banimento permanente.

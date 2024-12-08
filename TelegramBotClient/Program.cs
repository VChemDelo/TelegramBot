using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7844374796:AAGWOsO5uCx3OpGEPhaXPQ00GhHSoaTHKg0", cancellationToken: cts.Token);
var me = await bot.GetMe();
bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop bot.

// method to handle errors in polling or in your OnMessage/OnUpdate code
async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception); //exception to the console
}

// method that handle messages received by the bot:
async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text == "/start")
    {
        await bot.SendMessage(msg.Chat, "Привутствую! Выбери одиз из путей",
            replyMarkup: new InlineKeyboardMarkup().AddButtons("Право", "Лево"));
    }
}

// method that handle other types of updates received by the bot:
async Task OnUpdate(Update update)
{
    if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
    {
        if (query.Data == "Право")
        {
            await bot.AnswerCallbackQuery(query.Id, $"Вы выбрали {query.Data}");
            await bot.SendMessage(query.Message!.Chat, $"Направо пойдешь счатье найдешь!");
        }
        else
        {
            await bot.AnswerCallbackQuery(query.Id, $"Вы выбрали {query.Data}");
            await bot.SendMessage(query.Message!.Chat, $"Налево пойдешь разработчиком станешь!");
        }
    }
}

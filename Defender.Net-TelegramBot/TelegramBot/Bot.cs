using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Newtonsoft.Json.Linq;
using OverwatchAPI;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
class Bot
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    MainLoop().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MAIN LOOP EXIT ERROR - " + ex);
                    Thread.Sleep(30000);
                }
            }
        }

        static async Task MainLoop()
        {
            // Read Configuration
            var telegramKey = ConfigurationManager.AppSettings["TelegramKey"];

            // Start Bot
            var bot = new TelegramBotClient(telegramKey);
            var me = await bot.GetMeAsync();
            Console.WriteLine(me.Username + " started at " + DateTime.Now);

            var offset = 0;
            while (true)
            {
                var updates = new Update[0];
                try
                {
                    updates = await bot.GetUpdatesAsync(offset);
                }
                catch (TaskCanceledException)
                {
                    // Don't care
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR WHILE GETTIGN UPDATES - " + ex);
                }
                foreach (var update in updates)
                {
                    offset = update.Id + 1;
                    ProcessUpdate(bot, update, me);
                }

                await Task.Delay(1000);
            }
        }

        static async void ProcessUpdate(TelegramBotClient bot, Update update, User me)
        {
            // Process Request
            try
            {
                var httpClient = new ProHttpClient();
                var text = update.Message.Text;
                var replyText = string.Empty;
                var replyTextMarkdown = string.Empty;
                var replyImage = string.Empty;
                var replyImageCaption = string.Empty;
                var replyDocument = string.Empty;

                if (text != null && (text.StartsWith("/", StringComparison.Ordinal) || text.StartsWith("!", StringComparison.Ordinal)))
                {
                    // Log to console
                    Console.WriteLine(update.Message.Chat.Id + " < " + update.Message.From.Username + " - " + text);

                    // Allow ! or /
                    if (text.StartsWith("!", StringComparison.Ordinal))
                    {
                        text = "/" + text.Substring(1);
                    }

                    // Strip @BotName
                    text = text.Replace("@" + me.Username, "");

                    // Parse
                    string command;
                    string body;
                    if (text.StartsWith("/s/", StringComparison.Ordinal))
                    {
                        command = "/s"; // special case for sed
                        body = text.Substring(2);
                    }
                    else
                    {
                        command = text.Split(' ')[0];
                        body = text.Replace(command, "").Trim();
                    }
                    var stringBuilder = new StringBuilder();
                    switch (command.ToLowerInvariant())
                    {
                        case "/start":
                            try
                            {
                            }
                            catch
                            {
                                replyText = "Щось пішло не так. Зв'яжіться з адміністрацією сайта Defender.Net";
                            }
                            break;
                    }

                    // Output
                    replyText += stringBuilder.ToString();
                    if (!string.IsNullOrEmpty(replyText))
                    {
                        Console.WriteLine(update.Message.Chat.Id + " > " + replyText);
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, replyText);
                    }
                    if (!string.IsNullOrEmpty(replyTextMarkdown))
                    {
                        Console.WriteLine(update.Message.Chat.Id + " > " + replyTextMarkdown);
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, replyTextMarkdown, false, false, 0, null, ParseMode.Markdown);
                    }

                    if (!string.IsNullOrEmpty(replyImage) && replyImage.Length > 5)
                    {
                        Console.WriteLine(update.Message.Chat.Id + " > " + replyImage);
                        await bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
                        try
                        {
                            var stream = httpClient.DownloadData(replyImage).Result;
                            var extension = ".jpg";
                            if (replyImage.Contains(".gif") || replyImage.Contains("image/gif"))
                            { 
                                extension = ".gif";
                            }
                            else if (replyImage.Contains(".png") || replyImage.Contains("image/png"))
                            { 
                                extension = ".png";
                            }
                            else if (replyImage.Contains(".tif"))
                            { 
                                extension = ".tif";
                            }
                            else if (replyImage.Contains(".bmp"))
                            { 
                                extension = ".bmp";
                            }
                            var photo = new FileToSend("Photo" + extension, stream);
                            await bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.UploadPhoto);
                            if (extension == ".gif")
                            { 
                                await bot.SendDocumentAsync(update.Message.Chat.Id, photo);
                            }
                            else
                            { 
                                await bot.SendPhotoAsync(update.Message.Chat.Id, photo, replyImageCaption == string.Empty ? replyImage : replyImageCaption);
                            }
                        }
                        catch (System.Net.Http.HttpRequestException ex)
                        {
                            Console.WriteLine("Unable to download " + ex.HResult + " " + ex.Message);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, replyImage);
                        }
                        catch (System.Net.WebException ex)
                        {
                            Console.WriteLine("Unable to download " + ex.HResult + " " + ex.Message);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, replyImage);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(replyImage + " Threw: " + ex.Message);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, replyImage);
                        }
                    }

                    if (!string.IsNullOrEmpty(replyDocument) && replyDocument.Length > 5)
                    {
                        Console.WriteLine(update.Message.Chat.Id + " > " + replyDocument);
                        await bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.UploadDocument);
                        var stream = httpClient.DownloadData(replyDocument).Result;
                        var filename = replyDocument.Substring(replyDocument.LastIndexOf("/", StringComparison.Ordinal));
                        var document = new FileToSend(filename, stream);
                        await bot.SendDocumentAsync(update.Message.Chat.Id, document);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Console.WriteLine("Unable to download " + ex.HResult + " " + ex.Message);
                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Download error");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR - " + ex);
            }
        }

        static async void DelayedMessage(TelegramBotClient bot, long chatId, string message, int minutesToWait)
        {
            await Task.Delay(minutesToWait * 60000);
            await bot.SendTextMessageAsync(chatId, message);
        }
    }
}
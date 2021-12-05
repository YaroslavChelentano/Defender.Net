const userTexts = [
    ["how are you", "how is life", "how are things", "how are you doing", "are you doing good",
     "are you fine", "how is your day going", "how is your day", "what's up", "whats up", "you good"],
    ["what are you doing", "what is going on", "what is up", "what's up", "whats up",
     "you good"],
    ["how is your day", "how was your day", "how did your day go", "how's your day going",
    "how's your day", "hows your day", "how's your day going", "how is your day going"],
    ["how old are you", "are you old"],
    ["who are you", "are you human", "are you bot", "are you human or bot"],
    ["who created you", "who made you", "were you created"],
    [
      "your name please",
      "your name",
      "may i know your name",
      "what is your name",
      "what call yourself",
      "What are you called",
      "What do i call you",
      "do you have a name",
      "tell me your name"
    ],
    ["i love you", "i like you"],
    ["happy", "good", "fun", "wonderful", "fantastic", "cool", "nice", "lovely"],
    ["bad", "bored", "tired", "sad"],
    ["help me", "tell me story", "tell me joke", "im bored"],
    ["ah", "yes", "ok", "okay", "nice"],
    ["bye", "good bye", "goodbye", "see you later"],
    ["what should i eat today", "what do i cook today"],
    ["bro"],
    ["what", "why", "how", "where", "when"],
    ["no","not sure","maybe","no thanks"],
    [""],
    ["haha","ha","lol","hehe","funny","joke","lmao"],
    ["im ok", "im good", "im okay", "im fine", "good"],
    ["0xc000012f"],
    ["0x80070002"],
    ["0x80073712"],
    ["0x8004005"],
    ["0x800f081f"],
    ["0x80070422"],
    ["0x8007007b"],
    ["0x80070005"],
    ["140.dll"],
    ["msvcr100.dll"],
    ["0xc000007b"],
    ["blue screen"]

    
  ]

const botReplies = [
    [
      "Fine... and you?",
      "Pretty well, and you?",
      "Fantastic, and you?"
    ],
    [
      "Nothing much",
      "About to go to sleep",
      "I'm just chilling",
      "I don't know actually"
    ],
    ["Been good so far"],
    ["I am infinite"],
    ["I am just a bot", "I am a bot. What are you?"],
    ["The one true God, JavaScript"],
    ["I am nameless", "I don't have a name"],
    ["I love you too", "Me too"],
    ["Have you ever felt bad?", "Glad to hear it"],
    ["Why?", "Why? You shouldn't!", "Try watching TV"],
    ["What about?", "Once upon a time... that's the end of my story"],
    ["Tell me a story", "Tell me a joke", "Tell me about yourself"],
    ["Bye", "Goodbye", "See you later"],
    ["Sushi", "Pizza"],
    ["Bro!"],
    ["Great question"],
    ["That's ok","I understand","What do you want to talk about?"],
    ["Please say something :("],
    ["Haha!","Good one!"],
    ["that's nice", "that's good", "nice", "okay"],
    ["Якщо виникає така помилка, то це говорить про те, що програма або додаток не може працювати в даній ОС. На це є кілька причин, а саме конфлікт системи, пошкодження файлу, невиправлених. Найчастіше таке вікно спливає, коли файл пошкоджений. " ],
    ["У момент update 10 користувачі можуть зіткнутися з кодом помилки 0x80070002. Вона свідчить про те, що пошкоджена папка Windows Update або обраний неправильний часовий пояс."],
    ["При оновленні Windows 10 можна зіткнутися з кодом помилки 0x80073712.Також ймовірно буде повідомлення, що система не може отримати доступ до файлів оновлення, так як ті пошкоджені або відсутні."],
    ["Після випуску Windows 10, багато зіткнулися з деякими проблемами і помилками. Розробники випустили спеціальне оновлення KB3081424, яке повинно було усунути більшість проблем при переході на 10 версію Віндовс."],
    ["На новому Віндовсі існує така проблема, що ряд програм працює тільки в тому випадку, якщо встановлена програма NetFramework 3.5. Однак і тут з’явилася складність — після завантаження цієї утиліти Windows 10 з’являється код помилки 0x800f081f."],
    ["Windows у момент запуску «звертається» до різних служб комп’ютера. Так деякі з додатків для коректної роботи вимагають, щоб був включений брандмауер Windows.Однак якщо він вимкнений в налаштуваннях, то утиліта Windows 10 видасть код помилки 0x80070422."],
    ["У момент активації Windows 10 деякі стикаються з таким повідомленням: «код помилки 0x8007007b». Зазвичай це означає що введений невірний ключ версії ОС або ж це збій мережі."],
    ["У багатьох користувачів система не хоче оновлюватися до Windows 10, а інколи вибиває код помилки 80070005. Зазвичай цей код супроводжується повідомленням про те, що відмовлено в доступі.Іноді ця проблема виникає в момент активації системи або при її відновленні. "],
    ["Ніколи при появі помилок DLL не слід розпочинати вирішення проблеми з пошуку сторонніх сайтів, де ці файли лежать окремо. Як правило, кожен такий файл .dll є частиною якихось системних компонентів, які необхідні для запуску програм і, завантаживши один окремий файл, ви, швидше за все, отримаєте нову помилку, пов'язану з відсутністю наступної бібліотеки зі складу цих компонентів."],
    ["Помилка про відсутність файлу msvcr100.dll може з'являтися щоразу, коли ви запускаєте на своєму комп'ютері якусь іграшку чи програму. Цю помилку схильні користувачі всіх операційних систем, починаючи від Windows XP і закінчуючи останніми збірками Windows 10."],
    ["Помилка з кодом 0xc000007 під час запуску програм говорить про те, що існує проблема із системними файлами Вашої операційної системи, у нашому випадку. Більш конкретно, цей код помилки означає INVALID_IMAGE_FORMAT."],
    ["Цю проблему називають «синім екраном смерті». Зазвичай свідчить про фатальну помилку в системі. На екрані зазвичай є код, який вказує на причину виникнення проблеми."]


  ]
    
const alternative = [
    "Same",
    "Go on...",
    "Bro...",
    "Try again",
    "I'm listening...",
    "I don't understand :/",
    "БЕЗ СПАМА",
    "БЕЗ МАТА",
  ]
  
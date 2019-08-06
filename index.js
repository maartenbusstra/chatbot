const TelegramBot = require('node-telegram-bot-api');
const token = '';
const Timestamp = require('./Timestamp');
const Game = require('./Game');
const bot = new TelegramBot(token, { polling: true });

const TEST = true;
const TEST_TIME = 1565119260; // 21:21

const getTimeDataFromMessage = (msg, match) => {
  const d = new Date(TEST ? TEST_TIME * 1000 : msg.date * 1000);

  return {
    hours: d.getHours(),
    minutes: d.getMinutes(),
    messageMinutes: parseInt(match[2], 10),
    messageHours: parseInt(match[1], 10),
  };
};

bot.onText(/^(\d\d):(\d\d)$/, async (msg, match) => {
  try {
    const chatId = msg.chat.id;
    const game = await Game.getGame(chatId);
    const {
      hours,
      minutes,
      messageMinutes,
      messageHours,
    } = getTimeDataFromMessage(msg, match);

    const isMatch =
      hours === messageHours && minutes === messageMinutes && hours === minutes;

    if (!isMatch) {
      bot.sendMessage(msg.chat.id, `fail, no hours/minutes match :(`);
      return;
    }

    const ts = new Timestamp({ hours: hours, minutes: minutes });
    game.ensureMember(msg.from.id, { name: msg.from.first_name });
    game.addScore(msg.from.id, ts.score());

    bot.sendMessage(
      msg.chat.id,
      `sweeeet 🐐 ${ts.score()} point(s) for user ${msg.from.first_name} (${
        msg.from.id
      })

current scores:
${game.members.map(m => `${m.profile.name}: ${m.score}\n`)}`,
    );
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
});

bot.on('message', async msg => {
  //   console.log(msg);
  //   const chatId = msg.chat.id;
  //   try {
  //     const game = await Game.getGame(chatId);
  //     console.log(game);
  //     bot.sendMessage(
  //       msg.chat.id,
  //       `current scores:
  // ${game.members.map(m => `${m.profile.name}: ${m.score}\n`)}`,
  //     );
  //   } catch (e) {
  //     console.log(e);
  //   }
});

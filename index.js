const TelegramBot = require('node-telegram-bot-api');
const token = '';
const Timestamp = require('./Timestamp');
const Game = require('./Game');
const bot = new TelegramBot(token, { polling: true });

const TEST = false;
const TEST_TIME = 1565119260; // 21:21

const getTimeDataFromMessage = (msg, match) => {
  const d = new Date((TEST ? TEST_TIME : msg.date) * 1000);

  return {
    date: d,
    hours: d.getHours(),
    minutes: d.getMinutes(),
    messageMinutes: parseInt(match[2], 10),
    messageHours: parseInt(match[1], 10),
  };
};

bot.onText(/^\/clock setscore (\d+)/, async (msg, match) => {
  try {
    const chatId = msg.chat.id;
    const game = await Game.getGame(chatId);
    const score = parseInt(match[1], 10);
    game.setScore(msg.from, score);
    bot.sendMessage(chatId, `set score of ${msg.from.first_name} to ${score}`);
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
});

bot.onText(/^\/clock state/, async (msg, match) => {
  try {
    const chatId = msg.chat.id;
    const game = await Game.getGame(chatId);
    bot.sendMessage(chatId, JSON.stringify(game.game, null, 2));
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
});

bot.onText(/^(\d\d):(\d\d)$/, async (msg, match) => {
  try {
    const chatId = msg.chat.id;
    const game = await Game.getGame(chatId);
    const {
      date,
      hours,
      minutes,
      messageMinutes,
      messageHours,
    } = getTimeDataFromMessage(msg, match);

    const isMatch =
      hours === messageHours &&
      minutes === messageMinutes &&
      messageHours === messageMinutes;

    if (!isMatch) {
      bot.sendMessage(msg.chat.id, `fail, no hours/minutes match :(`);
      return;
    }

    const ts = new Timestamp({
      date,
      hours: messageHours,
      minutes: messageMinutes,
    });
    game.ensureMember(msg.from);
    try {
      game.addScore(msg.from.id, ts);
      bot.sendMessage(
        msg.chat.id,
        `sweeeet 🐐 ${ts.score()} point(s) for user ${msg.from.first_name} (${
          msg.from.id
        })
  
current scores:
${game.members.map(m => `${m.profile.first_name}: ${m.score}\n`)}`,
      );
    } catch (e) {
      bot.sendMessage(msg.chat.id, `Whoops, timestamp already claimed 🤷‍♂️`);
    }
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
});

// bot.on('message', async msg => {

// });

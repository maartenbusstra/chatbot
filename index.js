const Discord = require('discord.js');
const Timestamp = require('./Timestamp');
const Game = require('./Game');

const client = new Discord.Client();

const TEST = true;
const TEST_TIME = 1565119260000; // 21:21

const rTIME = /^(\d\d):(\d\d)$/;
const rSTATE = /^\/clock state/;

const getTimeDataFromMessage = (msg, match) => {
  const d = new Date(TEST ? TEST_TIME : msg.createdTimestamp);

  return {
    date: d,
    hours: d.getHours(),
    minutes: d.getMinutes(),
    messageMinutes: parseInt(match[2], 10),
    messageHours: parseInt(match[1], 10),
  };
};

client.on('ready', () => {
  console.log(`Logged in as ${client.user.tag}!`);
});

client.on('message', (msg) => {
  if (msg.author.id === '693794353621106708') return;

  let match = rTIME.exec(msg.content);
  if (match) handleMove(msg, match);
  match = rSTATE.exec(msg.content);
  if (match) handleState(msg, match);

  // msg.reply(JSON.stringify(getTimeDataFromMessage(msg)));
});

client.login();

async function handleMove(msg, match) {
  try {
    const chatId = msg.channel.id;
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
      msg.reply(`fail, no hours/minutes match :(`);
      return;
    }

    const ts = new Timestamp({
      date,
      hours: messageHours,
      minutes: messageMinutes,
    });
    game.ensureMember(msg.author);
    try {
      game.addScore(msg.author.id, ts);
      msg.reply(
        `sweeeet 🐐 ${ts.score()} point(s) for user ${msg.author.username} (${
          msg.author.id
        })

current scores:
${game.members.map((m) => `${m.profile.username}: ${m.score}\n`)}`,
      );
    } catch (e) {
      msg.reply(`Whoops, timestamp already claimed 🤷‍♂️`);
    }
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
}

// bot.onText(/^\/clock setscore (\d+)/, async (msg, match) => {
//   try {
//     const chatId = msg.chat.id;
//     const game = await Game.getGame(chatId);
//     const score = parseInt(match[1], 10);
//     game.setScore(msg.from, score);
//     msg.reply(chatId, `set score of ${msg.from.first_name} to ${score}`);
//   } catch (e) {
//     console.log('error', e);
//     console.log(e);
//   }
// });

async function handleState(msg) {
  try {
    const chatId = msg.channel.id;
    const game = await Game.getGame(chatId);
    msg.reply(JSON.stringify(game.game, null, 2));
  } catch (e) {
    console.log('error', e);
    console.log(e);
  }
}

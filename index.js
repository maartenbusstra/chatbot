const Bot = require('./Bot');
const Discord = require('./services/Discord');

const bot = new Bot({
  service: new Discord({
    token: 'NjkzNzk0MzUzNjIxMTA2NzA4.XoiMYw.54JFAHgEeyd28FPP1hKGch3w1i0',
  }),
});

bot.start();

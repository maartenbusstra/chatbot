module.exports = class Message {
  constructor({
    id,
    user: { id: userId, name },
    chatId,
    content,
    createdAt,
    reply = () => console.error('no reply handler given to message'),
  }) {
    this.id = id;
    this.user = { id: userId, name };
    this.content = content;
    this.chatId = chatId;
    this.createdAt = createdAt;
    this.reply = (...args) => reply(...args);
  }
};

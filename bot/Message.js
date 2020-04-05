module.exports = class Message {
  constructor({ id, user: { id: userId, name }, chatId, content, createdAt }) {
    this.id = id;
    this.user = { userId, name };
    this.content = content;
    this.chatId = chatId;
    this.createdAt = createdAt;
  }
};

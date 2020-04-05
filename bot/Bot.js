module.exports = class Bot {
  constructor({ connector, apps, storage }) {
    this.connector = connector;
    this.apps = apps;
    this.storage = storage;
  }

  async init() {
    await this.connector.connect((...args) => this.handleMessage(...args));
  }

  async start() {
    const { storage } = this;
    this.processes = this.apps.map((app) =>
      app.init({
        storage: {
          getItem(key) {
            return storage.getItem(`${app.name}:${key}`);
          },
          setItem(key, value) {
            return storage.setItem(`${app.name}:${key}`, value);
          },
        },
      }),
    );
    await this.init();
  }

  async handleMessage(message) {
    this.processes.forEach((process) =>
      process.commands.forEach(({ command, handler }) => {
        const match = command.exec(message.content);
        if (match) handler(match, message);
      }),
    );
  }
};

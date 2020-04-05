const Message = require('./Message');

module.exports = class Bot {
  constructor({ connector, apps }) {
    this.connector = connector;
    this.apps = apps;
  }

  async init() {
    this.connector.subscribe((...args) => this.handleMessage(...args));
    await this.connector.connect();
  }

  async start() {
    this.processes = this.apps.map((app) => app.init());
    await this.init();
  }

  async handleMessage(message) {
    this.processes.forEach((process) =>
      process.commands.forEach(({ command, handler }) => {
        console.log(command, message);

        const match = command.exec(message.content);
        if (match) handler(match, message);
      }),
    );
  }
};

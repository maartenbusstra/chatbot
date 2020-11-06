const TEST = process.env.TEST === 'true';

module.exports = class Move {
  constructor({ date, hours, minutes, messageMinutes, messageHours }) {
    this.date = date;
    this.hours = hours;
    this.minutes = minutes;
    this.messageHours = messageHours;
    this.messageMinutes = messageMinutes;
  }

  score() {
    if (TEST) return 1;
    if (this.hours !== this.minutes) return 0;
    if (this.hours === 0 || this.hours == 11 || this.hours === 22) return 2;
    return 1;
  }

  isValid() {
    return (
      !!TEST ||
      (this.hours === this.messageHours &&
        this.minutes === this.messageMinutes &&
        this.messageHours === this.messageMinutes)
    );
  }

  toUniqueId() {
    return [
      this.date.getDate(),
      this.date.getMonth(),
      this.date.getFullYear(),
      this.date.getHours(),
      this.date.getMinutes(),
    ].join('|');
  }
};

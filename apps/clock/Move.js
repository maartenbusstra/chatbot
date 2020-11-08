const SPECIAL_MOVES = {
  '00:00': 2,
  '11:11': 2,
  '22:22': 2,

  '12:34': 1,
  '13:37': 3,
};

const padTimePart = part => {
  let str = part.toString();
  return str.length === 1 ? `0${str}` : str;
};

module.exports = class Move {
  constructor({ date, hours, minutes, messageMinutes, messageHours }) {
    this.date = date;
    this.hours = hours;
    this.minutes = minutes;
    this.messageHours = messageHours;
    this.messageMinutes = messageMinutes;
  }

  score() {
    if (this.isSpecialMove()) return SPECIAL_MOVES[this.toString()];
    if (this.hours !== this.minutes) return 0;
    return 1;
  }

  toString() {
    return [this.messageHours, this.messageMinutes].map(padTimePart).join(':');
  }

  isSpecialMove() {
    return Object.keys(SPECIAL_MOVES).indexOf(this.toString()) > -1;
  }

  isValid() {
    if (!(this.hours === this.messageHours && this.minutes === this.messageMinutes)) {
      return false;
    }
    return this.messageHours === this.messageMinutes || this.isSpecialMove();
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

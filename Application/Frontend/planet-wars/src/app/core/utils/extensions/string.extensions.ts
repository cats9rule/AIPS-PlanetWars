interface String {
  isNullOrUndefined(): boolean;
  isBlankOrEmpty(): boolean;
}

String.prototype.isNullOrUndefined = function () {
  return this == null || this == undefined;
};

String.prototype.isBlankOrEmpty = function () {
  return this.search(/^\s*$/) == -1;
};

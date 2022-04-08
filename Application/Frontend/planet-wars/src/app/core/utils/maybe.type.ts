export type Maybe<T> = T | undefined | null;

export function isDefined<T>(object: Maybe<T>): boolean {
    if(object == undefined || object == null) return false;
    return true;
}
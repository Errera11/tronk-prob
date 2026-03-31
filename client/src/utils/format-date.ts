export function formatDate(date: string) {
  const dayjs = useDayjs()

  return dayjs(date).format('DD MMMM YYYY')
}

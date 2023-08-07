import { useCallback } from 'react'
import { isEmpty } from 'lodash'

import moment from 'moment'

import 'moment/locale/pt-br'

moment.locale('pt-br')

const useUtils = () => {
  const getOnlyNumber = useCallback((value = '') => value.replace(/\D/g, ''), [])

  const isBlank = useCallback((value) => isEmpty(value) || value.trim().length === 0, [])

  const formatCEP = useCallback((value) => !isBlank(value) && value.replace(/(\d{2})(\d{3})(\d{3})/, '$1.$2-$3'), [isBlank])

  const formatCPF = useCallback((value) => !isBlank(value) && value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4'), [isBlank])

  const formatCNPJ = useCallback((value) => !isBlank(value) && value.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5'), [isBlank])

  const formatSUSEP = useCallback((value) => !isBlank(value) && value.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/, '$1.$2.$3-$4'), [isBlank])

  const formatHours = useCallback((value) => value && moment(value).format('LT'), [])

  const formatDate = useCallback((value) => value && moment(value).format('DD/MM/YYYY'), [])

  const formatDateApi = useCallback((value) => value && moment(value).toISOString(), [])

  const formatDateNews = useCallback((value) => value && `${moment(value).format('DD')} de ${moment(value).format('MMMM YYYY')}`, [])

  const formatPhoneOrCellphone = useCallback((value) => {
    if (!isBlank(value)) {
      let number = getOnlyNumber(value)
      number = number.replace(/^(\d{2})(\d)/g, '($1) $2')
      number = number.replace(/(\d)(\d{4})$/, '$1-$2')
      return number
    }

    return ''
  }, [isBlank, getOnlyNumber])

  const formatCurrency = useCallback((value) => {
    let formatValue

    const currency = new Intl.NumberFormat([], {
      style: 'currency',
      currency: 'BRL',
    })

    if (!isBlank(value.toString())) {
      formatValue = currency.format(value.toString())
    } else {
      formatValue = 'R$ 0,00'
    }

    return formatValue
  }, [isBlank])

  return {
    isBlank,
    formatCPF,
    formatCEP,
    formatDate,
    formatCNPJ,
    formatHours,
    formatDateApi,
    getOnlyNumber,
    formatDateNews,
    formatPhoneOrCellphone,
    formatSUSEP,
    formatCurrency,
  }
}

export default useUtils

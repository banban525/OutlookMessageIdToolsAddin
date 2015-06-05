
$key = get-ItemProperty -path registry::HKEY_CURRENT_USER\Software\banban525\MessageIDTools

$productCode = $key.ProductCode

Start-Process "msiexec" "/quiet /uninstall $productCode" -Wait

Write-Host "Running tests..."
dotnet test .\MyDemoProject.sln --no-build --verbosity normal
$testExitCode = $LASTEXITCODE

if ($testExitCode -eq 0) {
    Write-Host "✅ All tests passed."

    $currentBranch = (git rev-parse --abbrev-ref HEAD).trim()
        git add .
       git commit --allow-empty -m "Trigger GitHub Actions"
       git push origin main
        Write-Host "🚀 Code pushed to main branch successfully."

} else {
    Write-Host "❌ Some tests failed. Aborting push to main."
    exit 1
}
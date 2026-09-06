PROJECT := project.csproj
CONFIG  ?= Debug
RID     ?= osx-arm64

RIDS := osx-arm64 win-x64

PUBLISH_TARGETS := $(addprefix publish-,$(RIDS))

.PHONY: build run publish publish-aot clean $(PUBLISH_TARGETS) $(AOT_TARGETS)

build:
	dotnet build $(PROJECT) -c $(CONFIG)

run:
	dotnet run --project $(PROJECT) -c $(CONFIG)

# --- Single-file, self-contained. Cross-compiles from any host. ---

publish: $(PUBLISH_TARGETS)

$(PUBLISH_TARGETS): publish-%:
	dotnet publish $(PROJECT) -c Release -r $* -o publish/$*

clean:
	rm -rf publish bin obj
